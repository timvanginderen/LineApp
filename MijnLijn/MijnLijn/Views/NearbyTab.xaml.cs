using MijnLijn.Models;
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

        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {

        }
    }
}