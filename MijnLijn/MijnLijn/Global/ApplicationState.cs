using Plugin.Geolocator.Abstractions;

namespace MijnLijn.Global
{
    public class ApplicationState
    {
        public static int[] FavoriteStopNumbers { get; set; }
        public Position CurrentLocation { get; set; }
    }
}
