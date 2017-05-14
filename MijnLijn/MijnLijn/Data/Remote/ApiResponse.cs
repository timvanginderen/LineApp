using System.Collections.Generic;

namespace MijnLijn.Models
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public Data Data { get; set; }
        public long Time { get; set; }
        public string Server { get; set; }
        public string ServerName { get; set; }
        public string Version { get; set; }
        public string Platform { get; set; }
    }

    public class Data
    {
        public List<Line> Lines { get; set; }
    }
}
