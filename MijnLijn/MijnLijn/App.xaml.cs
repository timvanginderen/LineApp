using System;
using System.Diagnostics;
using MijnLijn.Data.Local;
using MijnLijn.Data.Remote;
using MijnLijn.Global;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;

namespace MijnLijn
{
    public partial class App : Application
    {
        private static MijnLijnDatabase _database;
        public static LineManager LineManager { get; private set; }
        private static ApplicationState _applicationState;

        public App()
        {
            InitializeComponent();

            //TODO replace hardcoded favorites
            ApplicationState.FavoriteStopNumbers = new int[] { 105693, 105689, 305224 };

            GetLocationFromProperties();
            GetLocationFromGeoLocator();

            LineManager = new LineManager(new RestService());
            MainPage = new NavigationPage(new Views.MainPage());
        }

        public static ApplicationState ApplicationState => _applicationState ?? (_applicationState = new ApplicationState());

        public static MijnLijnDatabase Database => _database ?? 
            (_database = new MijnLijnDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("MijnLijn.db3")));


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        // Get last saved location from app properties
        // Save a default location in app state if none present in properties
        private static void GetLocationFromProperties()
        {
            Position position;

            if (Application.Current.Properties.ContainsKey(PropertyKeys.LocationLatitude))
            {
                position = new Position
                {
                    Latitude = (double)Application.Current.Properties[PropertyKeys.LocationLatitude],
                    Longitude = (double)Application.Current.Properties[PropertyKeys.LocationLongitude],
                    Timestamp = (DateTimeOffset)Application.Current.Properties[PropertyKeys.LocationTime],
                };
            }
            else
            {
                position = new Position
                {
                    Latitude = Constants.DefaultLocationLat,
                    Longitude = Constants.DefaultLocationLng
                };
            }

            App.ApplicationState.CurrentLocation = position;
        }

        // Get location via CrossGeolocator api
        // Save in App properties and Application State
        private static async void GetLocationFromGeoLocator()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;

                var position = await locator.GetPositionAsync(30000);
                if (position == null)
                    return;

                App.ApplicationState.CurrentLocation = position;

                Application.Current.Properties[PropertyKeys.LocationLatitude] = position.Latitude;
                Application.Current.Properties[PropertyKeys.LocationLongitude] = position.Longitude;
                Application.Current.Properties[PropertyKeys.LocationTime] = position.Timestamp;

                Debug.WriteLine("Position Status: {0}", position.Timestamp);
                Debug.WriteLine("Position Latitude: {0}", position.Latitude);
                Debug.WriteLine("Position Longitude: {0}", position.Longitude);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to get location, may need to increase timeout: " + ex);
            }
        }
    }
}
