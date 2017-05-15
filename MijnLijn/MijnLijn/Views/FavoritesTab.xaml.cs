using MijnLijn.Models;
using MijnLijn.Views;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MijnLijn
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoritesTab : ContentPage
    {

        private List<BusStop> stops;

        public FavoritesTab()
        {
            InitializeComponent();
            GetFavorites();
        }

        private async void GetFavorites()
        {
            //int[] favoriteStops = App.ApplicationState.FavoriteStopNumbers;
            //TODO replace hardcoded favorites
            stops = await App.Database.GetFavoriteHaltes(new int[] { 105693, 105689, 305224 });
            this.BindingContext = stops;
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