using System;
using MijnLijn.Global;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;

namespace MijnLijn.Utils
{
    public class PropertyHelper
    {
        // Get last saved location from app properties
        // Save a default location in app state if none present in properties
        public static Position GetLocationFromProperties()
        {
            Position position;

            if (Application.Current.Properties.ContainsKey(PropertyKeys.LocationLatitude))
            {
                position = new Position
                {
                    Latitude = (double)Application.Current.Properties[PropertyKeys.LocationLatitude],
                    Longitude = (double)Application.Current.Properties[PropertyKeys.LocationLongitude],
                    Timestamp = (DateTimeOffset)Application.Current.Properties[PropertyKeys.LocationTime],
                };
            }
            else
            {
                position = new Position
                {
                    Latitude = Constants.DefaultLocationLat,
                    Longitude = Constants.DefaultLocationLng
                };
            }

            return position;
        }
    }
}
