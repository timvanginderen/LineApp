using SQLite;
using System;

namespace MijnLijn.Models
{
    public class BusStop : BaseStop
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
        [Column("DISTANCE")]
        public string Distance { get; set; }

        public string DistanceFormatted
        {
            get
            {
                double dist;
                Double.TryParse(this.Distance, out dist);

                return string.Format("{0} meter", Math.Round(dist * 1000));
            }

        }

        public override int[] StopNumbers
        {
            get
            {
                int stopNumber;
                Int32.TryParse(this.Number, out stopNumber);
                return new int[1] { stopNumber };
            }
            set { }
        }
    }
}
