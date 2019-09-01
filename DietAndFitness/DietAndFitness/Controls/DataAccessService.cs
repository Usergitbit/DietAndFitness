using DietAndFitness.Core.Models;
using DietAndFitness.Core.Models.Composite;
using DietAndFitness.DatabaseContext;
using DietAndFitness.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DietAndFitness.Controls
{
    public class DataAccessService : IDataAccessService
    {
        private static SQLiteDbContext sqliteDbContext = new SQLiteDbContext();
        public async Task<int> Delete<T>(T entity) where T : class
        {
            try
            {
                sqliteDbContext.Remove(entity);
                await sqliteDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return 1;
        }

        public async Task<List<T>> GetAllAsync<T>() where T : DatabaseEntity, new()
        {
            return await sqliteDbContext.DbSet<T>().ToListAsync();
        }

        public async Task<List<T>> GetByDate<T>(DateTime date) where T : DatabaseEntity, new()
        {
            return await sqliteDbContext.DbSet<T>().Where(x => x.CreatedAt == date).ToListAsync();
        }

        public async Task<List<T>> GetByGUID<T>(string guid) where T : DatabaseEntity, new()
        {
            return await sqliteDbContext.DbSet<T>().Where(x => x.GUID == guid).ToListAsync();
        }

        public async Task<List<CompleteFoodItem>> GetCompleteItemAsync(DateTime date)
        {
            return await sqliteDbContext.DbSet<DailyFoodItem>()
                .Include(dfi => dfi.FoodItem)
                .Where(dfi => dfi.CreatedAt == date)
                .Select(x => new CompleteFoodItem { DailyFoodItem = x, LocalFoodItem = x.FoodItem })
                .ToListAsync();
        }

        public async Task<List<CompleteFoodItem>> GetCompleteItemAsync(DateTime startDate, DateTime endDate)
        {
            return await sqliteDbContext.DbSet<DailyFoodItem>()
                .Include(dfi => dfi.FoodItem)
                .Where(dfi => dfi.CreatedAt >= startDate && dfi.CreatedAt <= endDate)
                .Select(x => new CompleteFoodItem { DailyFoodItem = x, LocalFoodItem = x.FoodItem })
                .ToListAsync();
        }

        public async Task<Profile> GetCurrentProfile()
        {
            return await sqliteDbContext.Profiles.Include(p => p.DietFormula).Include(p => p.ProfileTypes).FirstOrDefaultAsync(x => x.StartDate <= DateTime.Today && x.EndDate >= DateTime.Today);
        }

        public void IncrementVersion()
        {

            var command = sqliteDbContext.Database.GetDbConnection().CreateCommand();
            sqliteDbContext.Database.OpenConnection();
            command.CommandText = @"update VersionItem
                                    set Number = Number + 1; ";
            command.ExecuteNonQuery();
        }

        public async Task<List<VersionItem>> GetVersionAsync()
        {
            return await sqliteDbContext.VersionItems.ToListAsync();
        }

        /// <summary>
        /// IF USING ONLY .ANY() IT CRASHES! SHOULD WRITE TEST FOR THIS
        /// </summary>
        /// <returns></returns>
        public bool HasProfiles()
        {
            var profiles = sqliteDbContext.Profiles.ToList();
            var foundProfile = profiles.Where(p => p.StartDate <= DateTime.Today && p.EndDate >= DateTime.Today).Any();
            return foundProfile;

        }

        public async Task<int> Insert<T>(T entity) where T : class
        {
            try
            {
                await sqliteDbContext.DbSet<T>().AddAsync(entity);
                await sqliteDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return 0;
        }

        public async Task<int> Update<T>(T entity) where T : class
        {
            try
            {
                await sqliteDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return 0;
        }

        public async Task<int> Update<T>(GlobalFoodItem item) where T : DatabaseEntity, new()
        {
            try
            {
                var result = await sqliteDbContext.LocalFoodItems.FirstOrDefaultAsync(gfi => gfi.GUID == item.GUID);
                LocalFoodItem castedItem = item;
                castedItem.ID = result.ID;
                sqliteDbContext.LocalFoodItems.Update(castedItem);
                await sqliteDbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return 0;
        }

        public async Task<List<LocalFoodItem>> GetByDescription(string description)
        {
            return await sqliteDbContext.LocalFoodItems.Where(lfi => lfi.Name.ToLower().Contains(description)
                                                                     || (lfi.Brand != null && lfi.Brand.ToLower().Contains(description))
                                                                     || (lfi.CookingMode != null && lfi.CookingMode.ToLower().Contains(description))).ToListAsync();

        }

        public void DiscardChanges()
        {
            sqliteDbContext.ChangeTracker.Entries()
                .Where(e => e.Entity != null).ToList()
                .ForEach(e => e.State = EntityState.Detached);
        }
    }
}
