using System.Collections.Generic;
using MijnLijn.Models;
using MijnLijn.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MijnLijn.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NearbyTab : ContentPage
    {

        private List<BusStop> _nearbyStops;

        public NearbyTab()
        {
            InitializeComponent();

            GetNearby();
        }

        // Get location from app state and get nearby stops from db
        private async void GetNearby()
        {
            //var position = App.ApplicationState.CurrentLocation ?? PropertyHelper.GetLocationFromProperties();
            var position = App.ApplicationState.CurrentLocation;
            _nearbyStops = await App.Database.GetHaltesByDistanceAsync(position);
            this.BindingContext = _nearbyStops;
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            // has been set to null, do not 'process' tapped event
            if (e == null) return; 

            BusStop lookup = (BusStop)e.Item;
            Navigation.PushAsync(new BusStopPage(lookup));

            // de-select the row
            ((ListView)sender).SelectedItem = null; 
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {

        }
    }
}