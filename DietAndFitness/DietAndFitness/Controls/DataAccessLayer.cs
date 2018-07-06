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
        /// <summary>
        /// Deletes item from table associated with the parameter class. Uses PrimaryKey property to find the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> Delete<T>(T entity)
        {
            return await database.DeleteAsync(entity);
        }
        /// <summary>
        /// Gets all items from the table associated with the parameter class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>List of items</returns>
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
        /// <summary>
        /// Gets all items from the table associated with the parameter class by date.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>List of items</returns>
        public Task<List<T>> GetByDate<T>(DateTime date) where T : DatabaseEntity, new()
        {
            return database.Table<T>().Where( x => x.CreatedAt == date).ToListAsync();  
        }
        /// <summary>
        /// Inserts into the table associated with the parameter class. ID should be null so it will be autoincremented.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Updates and entity in the table associated with the parameter class. Uses PrimaryKey property to find the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> Update<T>(T entity)
        {
            return await database.UpdateAsync(entity);
        }
        /// <summary>
        /// Executes a raw query and returns a list of the parameter class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">A list of items</param>
        /// <returns></returns>
        public async Task<List<T>> RawQuery<T>(string query) where T : DatabaseEntity, new()
        {
            return await database.QueryAsync<T>(query);
        }
        /// <summary>
        /// Gets an item from the table associated with the parameter class by GUID.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="guid"></param>
        /// <returns>List of one item</returns>
        public async Task<List<T>> GetByGUID<T>(string guid) where T : DatabaseEntity, new()
        {
            try
            {
                return await database.Table<T>().Where(x => x.GUID == guid ).ToListAsync();
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
        /// <summary>
        /// Gets the version information table.
        /// </summary>
        /// <returns>List of items</returns>
        public async Task<List<VersionItem>> GetVersion()
        {
            return await database.Table<VersionItem>().ToListAsync();
        }
        /// <summary>
        /// Updates a LocalFoodItem with values from a GlobalFoodItem.
        /// Should NOT be used on any table other than LocalFoodItem.
        /// </summary>
        /// <typeparam name="T">Should ONLY be LocalFoodItem</typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<int> Update<T>(GlobalFoodItem item) where T : DatabaseEntity, new()
        {
            List<T> result = await database.Table<T>().Where(x => x.GUID == item.GUID).ToListAsync();
            //implicit cast
            //needed as table are selected based on class attribute
            LocalFoodItem castedFoodItem = item;
            castedFoodItem.ID = result[0].ID;
            return await database.UpdateAsync(castedFoodItem);
           
        }

       
    }
}
