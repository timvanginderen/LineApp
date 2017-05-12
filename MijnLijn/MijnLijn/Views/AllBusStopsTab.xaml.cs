using MijnLijn.Models;
using MijnLijn.Views;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MijnLijn
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllBusStopsTab : ContentPage
    {
        public AllBusStopsTab()
        {
            InitializeComponent();

            GetLookups();
        }

        private async void GetLookups()
        {
            //List<BusStopLookup> haltes = await App.Database.GetHalteLookupsAsync();
            //this.BindingContext = haltes;
        }

        void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e == null) return; // has been set to null, do not 'process' tapped event

            BusStopLookup lookup = (BusStopLookup) e.Item;

            Navigation.PushAsync(new BusStopPage(lookup));
            //DisplayAlert("Test", lookup.Name, "cancel");

            ((ListView)sender).SelectedItem = null; // de-select the row
        }
    }
}