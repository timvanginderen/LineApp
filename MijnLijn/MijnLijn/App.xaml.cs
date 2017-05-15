
using MijnLijn.Data.Remote;
using Xamarin.Forms;

namespace MijnLijn
{
    public partial class App : Application
    {
        private static MijnLijnDatabase database;
        public static LineManager LineManager { get; private set; }
        public static ApplicationState ApplicationState { get; private set; }

        public App()
        {
            InitializeComponent();

            LineManager = new LineManager(new RestService());
            MainPage = new NavigationPage(new MainPage());
            ApplicationState = new ApplicationState();
            //TODO replace hardcoded favorites
            ApplicationState.FavoriteStopNumbers = new int[] { 105693, 105689, 305224 };
        }

        public static MijnLijnDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new MijnLijnDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("MijnLijn.db3"));
                }
                return database;
            }
        }

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
    }
}
