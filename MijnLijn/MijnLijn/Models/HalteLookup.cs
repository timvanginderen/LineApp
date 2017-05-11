using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MijnLijn.Models
{
    public class HalteLookup
    {
        [PrimaryKey, AutoIncrement]
        public int Z_PK { get; set; }
        public string ZNAME { get; set; }
    }
}
