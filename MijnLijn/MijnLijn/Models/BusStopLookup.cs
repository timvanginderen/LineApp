﻿using SQLite;

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
    }
}