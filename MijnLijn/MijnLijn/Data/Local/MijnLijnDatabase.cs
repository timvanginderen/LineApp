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
            double lat = 50;
            //string latStr = "50";
            double lng = 4;
            double fudge = Math.Pow(Math.Cos(ToRadians(lat)), 2);
            string sql = string.Format("SELECT * FROM [ZHALTE] " +
                    "ORDER BY ((zlatitude-{0})*(zlatitude-{1})) + " +
                    "((zlongitude - {2})*(zlongitude - {3}) * {4}) ASC", lat, lat, lng, lng, fudge);
            return database.QueryAsync<BusStop>(sql);
        }

        public double ToRadians(double degrees)
        {
            return degrees * 0.0174532925199432958;
        }




        //private Cursor getHaltesByDistanceCursor(Location location)
        //{
        //    SQLiteDatabase db = getReadableDatabase();
        //    String lat = String.valueOf(location.getLatitude());
        //    String lng = String.valueOf(location.getLongitude());
        //    String fudge =
        //            String.valueOf(Math.pow(Math.cos(Math.toRadians(location.getLatitude())), 2));
        //    String[] whereArgs = new String[] { lat, lat, lng, lng, fudge };

        //    String sql = "SELECT * FROM ZHALTE " +
        //            "ORDER BY ((zlatitude-?)*(zlatitude-?)) + " +
        //            "((zlongitude - ?)*(zlongitude - ?) * ?) ASC";

        //    Cursor c = db.rawQuery(sql, whereArgs);
        //    return c;
        //}

    }
}
