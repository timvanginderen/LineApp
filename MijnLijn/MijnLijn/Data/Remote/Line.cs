using System;

namespace MijnLijn.Models
{
    public class Line
    {
        public string LineNumber { get; set; }
        public string InternLineNumber { get; set; }
        public string Destination { get; set; }
        public int NormalTime { get; set; }
        public DateTime NormalTimeISO { get; set; }
        public int RealTime { get; set; }
        public DateTime RealTimeISO { get; set; }
        public string Delay { get; set; }
        public Color BackgroundColor { get; set; }
        public Color TextColor { get; set; }
        public string BackgroundColorHex
        {
            get
            {
                Xamarin.Forms.Color myColor = ToXamarinColor(this.BackgroundColor);
                return ToHex(myColor);
            }
        }
        public string TextColorHex
        {
            get
            {
                Xamarin.Forms.Color myColor = ToXamarinColor(this.TextColor);
                return ToHex(myColor);
            }
        }
        // Get arrival time in pretty format; display in minutes when close
        // Display in red if delayed (real time exceeds normal time)
        public string Arrival
        {
            get
            {
                //Check if arrival time is more than half an hour from now
                if ((this.RealTimeISO - DateTime.Today).Minutes > 30)
                {
                    return RealTimeISO.ToString("HH:mm");
                }
                else
                {
                    return RealTimeISO.ToString("mm") + "'";
                }

            }
        }
        private Xamarin.Forms.Color ToXamarinColor(Color color)
        {
            return Xamarin.Forms.Color.FromRgb(color.Red, color.Green, color.Blue);
        }

        private string ToHex(Xamarin.Forms.Color color)
        {
            var red = (int)(color.R * 255);
            var green = (int)(color.G * 255);
            var blue = (int)(color.B * 255);
            var alpha = (int)(color.A * 255);

            var hex = $"#{alpha:X2}{red:X2}{green:X2}{blue:X2}";

            return hex;
        }
    }

    public class Color
    {
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
        public int Alpha { get; set; }
    }
}
