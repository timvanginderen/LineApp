
using MijnLijn.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MijnLijn.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BusStopPage : ContentPage
    {
        public BusStopPage(BusStopLookup lookup)
        {
            InitializeComponent();

            this.Title = lookup.Name;
        }
    }
}