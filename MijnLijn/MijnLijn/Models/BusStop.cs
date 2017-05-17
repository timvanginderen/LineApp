using SQLite;
using System;

namespace MijnLijn.Models
{
    public class BusStop : BaseStop
    {
        [PrimaryKey, AutoIncrement, Column("Z_PK")]
        public int Id { get; set; }
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
        [Column("DISTANCE")]
        public string Distance { get; set; }

        public string DistanceFormatted
        {
            get
            {
                double.TryParse(Distance, out double dist);
                return $"{Math.Round(dist * 1000)} meter";
            }

        }

        public override int[] StopNumbers
        {
            get
            {
                int.TryParse(Number, out int stopNumber);
                return new [] { stopNumber };
            }
            set { }
        }
    }
}
