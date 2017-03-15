using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightAndDark
{
    public class StatisticsDatabase
    {
        // SQLite connection
        private SQLiteAsyncConnection database;

        public StatisticsDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Statistics>().Wait();
        }

        // Getting statistics data from database
        public Task<List<Statistics>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<Statistics>("SELECT * FROM [Statistics]");
        }

        public Task<List<Statistics>> GetItemsNotDoneAsync1()
        {
            return database.QueryAsync<Statistics>("SELECT * FROM [Statistics] WHERE ID = 1");
        }
        public Task<List<Statistics>> GetItemsNotDoneAsyncCharCheck(int check)
        {
            return database.QueryAsync<Statistics>("SELECT * FROM [Statistics] WHERE ID = '" + check + "'");
        }
        public Task<List<Statistics>> GetItemsNotDoneAsyncEnemyCheck(int enemycheck)
        {
            return database.QueryAsync<Statistics>("SELECT * FROM [Statistics] WHERE ID = '" + enemycheck + "'");
        }

        public Task<List<Statistics>> GetItemsNotDoneAsync2()
        {
            return database.QueryAsync<Statistics>("SELECT * FROM [Statistics] WHERE ID = 2");
        }

        public Task<List<Statistics>> GetItemsNotDoneAsync3()
        {
            return database.QueryAsync<Statistics>("SELECT * FROM [Statistics] WHERE ID = 3");
        }

        public Task<List<Statistics>> GetItemsNotDoneAsync4()
        {
            return database.QueryAsync<Statistics>("SELECT * FROM [Statistics] WHERE ID = 4");
        }

        public Task<List<Statistics>> UpdateItems(int HP)
        {
            return database.QueryAsync<Statistics>("UPDATE [Statistics] SET [HP] = '" + HP + "' ");
        }

        // Saving statistics data
        public Task<int> SaveItemAsync(Statistics item)
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
    }
}
