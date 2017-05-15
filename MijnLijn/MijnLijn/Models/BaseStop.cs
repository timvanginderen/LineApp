namespace MijnLijn.Models
{
    public abstract class BaseStop
    {
        abstract public int[] StopNumbers { get; set; }

        public bool Favorited
        {
            get
            {
                int[] favorites = App.ApplicationState.FavoriteStopNumbers;
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
