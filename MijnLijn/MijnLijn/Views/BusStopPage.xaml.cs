
using MijnLijn.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MijnLijn.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BusStopPage : ContentPage
    {
        public BusStopPage(BusStopLookup lookup)
        {
            InitializeComponent();

            this.Title = lookup.Name;

            GetLines();
            
        }

        async void GetLines()
        {
            ApiResponse apiResponse = await App.LineManager.GetLines();

            this.BindingContext = apiResponse.Data.Lines;

            Line line = apiResponse.Data.Lines[0];
            string backgroundHex = line.BackgroundColorHex;
            string textHex = line.TextColorHex;
            string dest = line.Destination;
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}