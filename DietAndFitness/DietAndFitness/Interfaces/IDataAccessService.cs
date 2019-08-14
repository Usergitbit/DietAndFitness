using DietAndFitness.Core.Models;
using DietAndFitness.Core.Models.Composite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DietAndFitness.Interfaces
{
    /// <summary>
    /// Interface for basic CRUD operations
    /// </summary>
    public interface IDataAccessService 
    {
    
        Task<List<T>> GetAllAsync<T>() where T : DatabaseEntity, new();
        Task<int> Insert<T>(T entity) where T : class;
        Task<int> Update<T>(T entity) where T : class;
        Task<int> Update<T>(GlobalFoodItem item) where T : DatabaseEntity, new();
        Task<int> Delete<T>(T entity) where T : class;
        Task<List<T>> GetByDate<T>(DateTime date) where T : DatabaseEntity, new();
        Task<List<T>> GetByGUID<T>(string guid) where T : DatabaseEntity, new();
        Task<List<VersionItem>> GetVersionAsync();
        void IncrementVersion();
        bool HasProfiles();
        Task<List<CompleteFoodItem>> GetCompleteItemAsync(DateTime date);
        Task<List<CompleteFoodItem>> GetCompleteItemAsync(DateTime startDate, DateTime endDate);
        Task<Profile> GetCurrentProfile();
        Task<List<LocalFoodItem>> GetByDescription(string description);
        void DiscardChanges();
    }
}
