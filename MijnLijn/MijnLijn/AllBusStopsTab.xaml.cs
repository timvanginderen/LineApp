using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MijnLijn.Models;

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
    }
}