using MijnLijn.Models;
using SQLite;
using System;
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

        public Task<List<BusStop>> GetHaltesByDistanceAsync()
        {
            //TODO replace hardcoded location 
            double lat = 50;
            double lng = 4;
            double fudge = Math.Pow(Math.Cos(ToRadians(lat)), 2);

            string sql = string.Format("SELECT *, ((zlatitude-{0})*(zlatitude-{1})) + " +
                                       "((zlongitude - {2})*(zlongitude - {3}) * {4}) " +
                                       "AS distance FROM [ZHALTE] ORDER BY distance ASC", 
                                       lat, lat, lng, lng, fudge);

            return database.QueryAsync<BusStop>(sql);
        }
        public Task<List<BusStop>> GetFavoriteHaltes(int[]stopNumbers)
        {
            string numbers = String.Join(",", stopNumbers);
            string sql = String.Format("SELECT * FROM ZHALTE WHERE ZNUMBER IN ({0})", numbers);

            return database.QueryAsync<BusStop>(sql);
        }
        //TODO move to util class
        public double ToRadians(double degrees)
        {
            return degrees * 0.0174532925199432958;
        }
    }
}
