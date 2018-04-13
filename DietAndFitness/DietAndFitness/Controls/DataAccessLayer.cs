using DietAndFitness.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Class that implements the interface for basic CRUD operations
/// Keeps the Model specific implementations separate from ViewModels for future database migrations
/// </summary>
namespace DietAndFitness.Controls
{
    public class DataAccessLayer<T> : IDataAccess<T> where T : DatabaseEntity, new()
    {
        private SQLiteAsyncConnection database;

        public DataAccessLayer(SQLiteAsyncConnection _database)
        {
            database = _database;
        }
        public async Task<int> Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> Get()
        {
            try
            {
                return await database.Table<T>().ToListAsync();
            }
            catch(Exception e)
            {
                Debug.WriteLine("Error at getting list from database" + e.Message + e.Source + e.StackTrace);
            }
            return null;
        }

        public async Task<int> Insert(T entity)
        {
            try
            {


                return await database.InsertAsync(entity);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error at inserting into database" + e.Source + e.Message + e.StackTrace);
            }
            return 0;
        }

        public async Task<int> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
