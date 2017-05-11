using MijnLijn.Models;
using System;
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

            this.BindingContext = new[] { "a", "b", "c" };
        }

        void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            DisplayAlert("Test", "Clicked", "cancel");

            if (e == null) return; // has been set to null, do not 'process' tapped event
            Debug.WriteLine("Tapped: " + e.Item);
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
            ZHALTELOOKUP halte = await App.Database.GetHalteLookupAsync(21177);
            await DisplayAlert("Title", halte.ZNAME, "Cancel");

        }
    }
}