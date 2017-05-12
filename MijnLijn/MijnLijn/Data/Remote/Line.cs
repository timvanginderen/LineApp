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
    }

    public class Color
    {
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
        public int Alpha { get; set; }
    }
}
