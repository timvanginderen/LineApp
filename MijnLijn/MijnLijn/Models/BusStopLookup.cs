using SQLite;
using System;
using System.Linq;

namespace MijnLijn.Models
{
    [Table("ZHALTELOOKUP")]
    public class BusStopLookup : BaseStop
    {
        [PrimaryKey, AutoIncrement, Column("Z_PK")]
        public int Id { get; set; }
        //[Column("ZNAME")]
        //public string Name { get; set; }
        [Column("ZNUMBERS")]
        public string Numbers { get; set; }
        [Column("ZSECTIONID")]
        public string SectionId{ get; set; }

        public string NumbersDisplay
        {
            get
            {
                if (this.Numbers.Length == 0) return "";
                return string.Join(",", this.Numbers.Split(';'));
            }
        }

        public int[] NumbersArray
        {
            get
            {
                if (this.Numbers.Length == 0) return new int[0];
                return this.Numbers.Split(';').Select(n => Convert.ToInt32(n)).ToArray();
            }

        }

        public override int[] StopNumbers
        {
            get
            {
                return this.NumbersArray;
            }
            set {  }
        }
    }
}
