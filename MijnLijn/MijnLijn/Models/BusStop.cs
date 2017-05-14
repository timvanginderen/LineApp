using SQLite;

namespace MijnLijn.Models
{
    public class BusStop
    {
        [PrimaryKey, AutoIncrement, Column("Z_PK")]
        public int Id { get; set; }
        [Column("ZNAME")]
        public string Name { get; set; }
        [Column("ZEXTERNALID")]
        public string ExternalId { get; set; }
        [Column("ZSECTIONID")]
        public string SectionId { get; set; }
        [Column("ZLATITUDE")]
        public double Latitude { get; set; }
        [Column("ZLONGITUDE")]
        public double Longitude { get; set; }
        [Column("ZNUMBER")]
        public string Number { get; set; }
    }
}
