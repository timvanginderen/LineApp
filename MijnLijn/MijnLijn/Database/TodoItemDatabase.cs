using MijnLijn.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MijnLijn
{
    public class TodoItemDatabase
    {
        readonly SQLiteAsyncConnection database;

        public TodoItemDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<ToDoItem>().Wait();
        }

        public Task<List<ToDoItem>> GetItemsAsync()
        {
            return database.Table<ToDoItem>().ToListAsync();
        }

        public Task<List<ToDoItem>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<ToDoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public Task<List<ZHALTELOOKUP>> GetHalteLookupsAsync()
        {
            return database.QueryAsync<ZHALTELOOKUP>("SELECT * FROM [ZHALTELOOKUP]");
        }

        public Task<ZHALTELOOKUP> GetHalteLookupAsync(int id)
        {
            //return database.Table<HalteLookup>().Where(i => i.Z_PK == id).FirstOrDefaultAsync();
            return database.GetAsync<ZHALTELOOKUP>("SELECT * FROM [ZHALTELOOKUP] LIMIT 1");
        }

        public Task<ToDoItem> GetItemAsync(int id)
        {
            return database.Table<ToDoItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(ToDoItem item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(ToDoItem item)
        {
            return database.DeleteAsync(item);
        }
    }
}
