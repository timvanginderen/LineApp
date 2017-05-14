using SQLite;

namespace MijnLijn.Models
{
    public class Favorite
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
    }
}
