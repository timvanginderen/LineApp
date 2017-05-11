using SQLite;

namespace MijnLijn.Models
{
    class Halte
    {
        [PrimaryKey, AutoIncrement]
        public int Z_PK { get; set; }
        public int ZEXTERNALID { get; set; }
        public string ZNAME { get; set; }
        public int NUMBER { get; set; }
    }
}
