using MijnLijn.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            List<ZHALTELOOKUP> haltes = await App.Database.GetHalteLookupsAsync();
            this.BindingContext = haltes;
        }

        void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e == null) return; // has been set to null, do not 'process' tapped event

            ZHALTELOOKUP lookup = (ZHALTELOOKUP) e.Item;

            DisplayAlert("Test", lookup.ZNAME, "cancel");

            ((ListView)sender).SelectedItem = null; // de-select the row
        }
    }
}