using SQLite;
using System.Text;

namespace MijnLijn.Models
{
    public class ZHALTELOOKUP
    {
        [PrimaryKey, AutoIncrement]
        public int Z_PK { get; set; }
        public string ZNAME { get; set; }
        public string ZNUMBERS { get; set; }
        public string ZSECTIONID { get; set; }
        public string Numbers
        {
            get
            {
                if (this.ZNUMBERS.Length == 0) return "";
                return string.Join(",", ZNUMBERS.Split(';'));
            }
        }
    }
}
