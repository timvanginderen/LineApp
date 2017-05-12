
using Xamarin.Forms;

namespace MijnLijn
{
    public partial class App : Application
    {
        static MijnLijnDatabase database;

        public App()
        {
            InitializeComponent();
            
            MainPage = new MainPage();
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
