using DietAndFitness.Models;
using DietAndFitness.ViewModels.Secondary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DietAndFitness.Controls
{
    /// <summary>
    /// Interface for basic CRUD operations
    /// </summary>
    public interface IDataAccess 
    {
    
        Task<List<T>> GetAllAsync<T>() where T : DatabaseEntity, new();
        Task<int> Insert<T>(T entity);
        Task<int> Update<T>(T entity);
        Task<int> Update<T>(GlobalFoodItem item) where T : DatabaseEntity, new();
        Task<int> Delete<T>(T entity);
        Task<List<T>> GetByDate<T>(DateTime date) where T : DatabaseEntity, new();
        Task<List<T>> GetByGUID<T>(string guid) where T : DatabaseEntity, new();
        Task<List<VersionItem>> GetVersion();
        bool HasProfiles();
        Task<List<CompleteFoodItem>> GetCompleteItemAsync();
        Task<Profile> GetCurrentProfile();
    }
}
