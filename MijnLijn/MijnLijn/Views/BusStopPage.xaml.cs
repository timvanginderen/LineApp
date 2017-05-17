
using MijnLijn.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MijnLijn.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BusStopPage : ContentPage
    {
        public BusStopPage(BaseStop stop)
        {
            InitializeComponent();

            // Use bus stop name as page title
            this.Title = stop.Name;

            // Fetch lines for stops from server
            int[] stopNumbers = stop.StopNumbers;
            GetLines(stopNumbers);
            
        }

        private async void GetLines(int[] stopNumbers)
        {
            ApiResponse apiResponse = await App.LineManager.GetLines(stopNumbers);

            if (apiResponse.Success)
            {
                this.BindingContext = apiResponse.Data.Lines;
            }
            else
            {
                await DisplayAlert("Info", "Deze halte bestaat niet meer", "Cancel");
            }
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
        }
    }
}