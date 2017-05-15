using MijnLijn.Models;
using MijnLijn.Views;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MijnLijn
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NearbyTab : ContentPage
    {

        private List<BusStop> nearbyStops;

        public NearbyTab()
        {
            InitializeComponent();

            GetNearby();
        }

        private async void GetNearby()
        {
            nearbyStops = await App.Database.GetHaltesByDistanceAsync();
            this.BindingContext = nearbyStops;
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e == null) return; // has been set to null, do not 'process' tapped event

            BusStop lookup = (BusStop)e.Item;
            Navigation.PushAsync(new BusStopPage(lookup));

            ((ListView)sender).SelectedItem = null; // de-select the row
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {

        }
    }
}