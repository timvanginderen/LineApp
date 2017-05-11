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

        async void OnSaveClicked(object sender, EventArgs e)
        {
            var todoItem = new ToDoItem();
            todoItem.Name = "TestName";
            await App.Database.SaveItemAsync(todoItem);
            //await Navigation.PopAsync();
        }

        async void OnShowClicked(object sender, EventArgs e)
        {
            //ToDoItem item = await App.Database.GetItemAsync(1);
            //ZHALTELOOKUP halte = await App.Database.GetHalteLookupAsync(21177);
            
            //await DisplayAlert("Title", halte.ZNAME, "Cancel");
            await DisplayAlert("Title", "einde", "Cancel");

        }
    }
}