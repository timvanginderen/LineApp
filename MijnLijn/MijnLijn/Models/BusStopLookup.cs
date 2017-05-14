using SQLite;
using System;
using System.Diagnostics;
using System.Linq;

namespace MijnLijn.Models
{
    [Table("ZHALTELOOKUP")]
    public class BusStopLookup
    {
        [PrimaryKey, AutoIncrement, Column("Z_PK")]
        public int Id { get; set; }
        [Column("ZNAME")]
        public string Name { get; set; }
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

        public bool Favorited
        {
            get
            {
                int[] favorites = App.ApplicationState.FavoriteStopNumbers;
                foreach (int myNumber in this.NumbersArray)
                {
                    foreach (int favoriteNumber in favorites)
                    {
                        if (myNumber == favoriteNumber)
                            return true;
                    }
                }
                return false;
            }
            set
            {
                Debug.WriteLine("joep");
            }
        }
    }
}
