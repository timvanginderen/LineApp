using System.Collections.Generic;
using System.Linq;
using MijnLijn.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MijnLijn.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllBusStopsTab : ContentPage
    {
        private List<BusStopLookup> _allLookups;
        private List<BusStopLookup> _filteredLookups;

        public AllBusStopsTab()
        {
            InitializeComponent();

            GetLookups();
        }

        private async void GetLookups()
        {
            _allLookups = await App.Database.GetHalteLookupsAsync();
            this.BindingContext = _allLookups;
        }

        void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            // has been set to null, do not 'process' tapped event
            if (e == null) return; 

            BusStopLookup lookup = (BusStopLookup) e.Item;
            Navigation.PushAsync(new BusStopPage(lookup));

            // de-select the row
            ((ListView)sender).SelectedItem = null;
        }

        private void AllStopsSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string query = AllStopsSearchBar.Text;

            if (_allLookups != null && _allLookups.Count > 0)
            {
                _filteredLookups = _allLookups.Where(lookup => lookup.Name.ToLower().Contains(query.ToLower())).ToList();
                this.BindingContext = _filteredLookups;
            }
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            string smt = e == null ? "" : e.ToString();
        }
    }
}