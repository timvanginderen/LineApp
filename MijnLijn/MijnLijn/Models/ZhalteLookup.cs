using SQLite;

namespace MijnLijn.Models
{
    public class ZHALTELOOKUP
    {
        [PrimaryKey, AutoIncrement]
        public int Z_PK { get; set; }
        public string ZNAME { get; set; }
    }
}
