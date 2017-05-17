using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MijnLijn.Models;
using MijnLijn.Utils;
using Plugin.Geolocator.Abstractions;
using SQLite;

namespace MijnLijn.Data.Local
{
    public class MijnLijnDatabase
    {
        private const string SelectAllLookups = "SELECT * FROM [ZHALTELOOKUP]";

        private readonly SQLiteAsyncConnection _database;

        public MijnLijnDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Favorite>().Wait();
        }

        public Task<List<BusStopLookup>> GetHalteLookupsAsync()
        {
            return _database.QueryAsync<BusStopLookup>(SelectAllLookups);
        }

        public Task<List<BusStop>> GetHaltesByDistanceAsync(Position position)
        {

            double lat = position.Latitude;
            double lng = position.Longitude;

            double fudge = Math.Pow(Math.Cos(MathUtil.ToRadians(lat)), 2);

            string sql = $"SELECT *, ((ZLATITUDE-{lat})*(ZLATITUDE-{lat})) + " +
                         $"((ZLONGITUDE - {lng})*(ZLONGITUDE - {lng}) * {fudge}) " +
                         "AS distance FROM [ZHALTE] ORDER BY distance ASC";

            return _database.QueryAsync<BusStop>(sql);
        }

        public Task<List<BusStop>> GetFavoriteHaltes(int[]stopNumbers)
        {
            string numbers = String.Join(",", stopNumbers);
            string sql = $"SELECT * FROM ZHALTE WHERE ZNUMBER IN ({numbers})";

            return _database.QueryAsync<BusStop>(sql);
        }
    }
}
