using MijnLijn.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MijnLijn.Database
{
    class HalteDatabase
    {
        readonly SQLiteAsyncConnection database;

        public HalteDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Halte>().Wait();
        }

        public Task<List<Halte>> GetItemsAsync()
        {
            return database.Table<Halte>().ToListAsync();
        }

        public Task<Halte> GetItemAsync(int id)
        {
            return database.Table<Halte>().Where(i => i.Z_PK == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Halte item)
        {
            if (item.Z_PK != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Halte item)
        {
            return database.DeleteAsync(item);
        }
    }
}
