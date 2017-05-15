using MijnLijn.Models;
using MijnLijn.Views;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MijnLijn
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllBusStopsTab : ContentPage
    {

        private List<BusStopLookup> allLookups;
        private List<BusStopLookup> filteredLookups;

        public AllBusStopsTab()
        {
            InitializeComponent();

            GetLookups();
        }

        private async void GetLookups()
        {
            allLookups = await App.Database.GetHalteLookupsAsync();
            this.BindingContext = allLookups;
        }

        void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e == null) return; // has been set to null, do not 'process' tapped event

            BusStopLookup lookup = (BusStopLookup) e.Item;
            Navigation.PushAsync(new BusStopPage(lookup));

            ((ListView)sender).SelectedItem = null; // de-select the row
        }

        private void AllStopsSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string query = AllStopsSearchBar.Text;

            if (allLookups != null && allLookups.Count > 0)
            {
                filteredLookups = allLookups.Where(lookup => lookup.Name.ToLower().Contains(query.ToLower())).ToList();
                this.BindingContext = filteredLookups;
            }
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            string smt = e == null ? "" : e.ToString();
        }
    }
}