using MijnLijn.Global;
using SQLite;

namespace MijnLijn.Models
{
    public abstract class BaseStop
    {
        [Column("ZNAME")]
        public string Name { get; set; }

        public abstract int[] StopNumbers { get; set; }

        public bool Favorited
        {
            get
            {
                int[] favorites = ApplicationState.FavoriteStopNumbers;
                foreach (int myNumber in this.StopNumbers)
                {
                    foreach (int favoriteNumber in favorites)
                    {
                        if (myNumber == favoriteNumber)
                            return true;
                    }
                }
                return false;
            }
        }
    }
}
