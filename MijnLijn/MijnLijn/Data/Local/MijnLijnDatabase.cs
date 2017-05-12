using MijnLijn.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MijnLijn
{
    public class MijnLijnDatabase
    {
        readonly SQLiteAsyncConnection database;

        public MijnLijnDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Favorite>().Wait();
        }

        public Task<List<BusStopLookup>> GetHalteLookupsAsync()
        {
            return database.QueryAsync<BusStopLookup>("SELECT * FROM [ZHALTELOOKUP]");
        }
       
    }
}
