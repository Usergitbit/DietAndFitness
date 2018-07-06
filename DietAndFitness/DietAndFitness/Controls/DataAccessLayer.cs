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
    /// <summary>
    /// Class that implements the interface for basic CRUD operations
    /// Keeps the Model specific implementations separate from ViewModels for future database migrations
    /// </summary>
    public class DataAccessLayer : IDataAccess 
    {
        private SQLiteAsyncConnection database;

        public DataAccessLayer(SQLiteAsyncConnection _database)
        {
            database = _database;
        }
        public async Task<int> Delete<T>(T entity)
        {
            return await database.DeleteAsync(entity);
        }

        public async Task<List<T>> GetAllAsync<T>() where T : DatabaseEntity, new()
        {
            try
            {
                return await database.Table<T>().ToListAsync();
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Error at getting list from database" + ex.Message + ex.Source + ex.StackTrace);
                throw ex;
            }
            
        }
        public Task<List<T>> GetByDate<T>(DateTime date) where T : DatabaseEntity, new()
        {
            return database.Table<T>().Where( x => x.CreatedAt == date).ToListAsync();  
            //return database.QueryAsync<T>("select * from dailyfooditem where createdat = '"+date+"'");
        }

        public async Task<int> Insert<T>(T entity)
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

        public async Task<int> Update<T>(T entity)
        {
            return await database.UpdateAsync(entity);
        }

        public async Task<List<T>> RawQuery<T>(string query) where T : DatabaseEntity, new()
        {
            return await database.QueryAsync<T>(query);
        }

        public async Task<List<T>> GetByGUID<T>(string guid) where T : DatabaseEntity, new()
        {
            try
            {
                return await database.Table<T>().Where(x => x.GUID == guid ).ToListAsync();
               return await database.QueryAsync<T>("select * from LocalFoodItem");
                
            }
            catch(InvalidOperationException ex)
            {
                Debug.WriteLine("Message: " + ex.Message + "Method: " + ex.TargetSite);
                return null;
            }
            catch (ArgumentException ex)
            {
                Debug.WriteLine("Message: " + ex.Message + "Method: " + ex.TargetSite);
                return null;
            }

        }

        public async Task<List<VersionItem>> GetVersion()
        {
            return await database.Table<VersionItem>().ToListAsync();
        }

        public async Task<int> Update<T>(GlobalFoodItem item) where T : DatabaseEntity, new()
        {
            List<T> result = await database.Table<T>().Where(x => x.GUID == item.GUID).ToListAsync();
            LocalFoodItem castedFoodItem = item;
            castedFoodItem.ID = result[0].ID;
            return await database.UpdateAsync(castedFoodItem);
           
        }

       
    }
}
