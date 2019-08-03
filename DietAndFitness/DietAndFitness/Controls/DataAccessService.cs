using DietAndFitness.Core.Models;
using DietAndFitness.Core.Models.Composite;
using DietAndFitness.DatabaseContext;
using DietAndFitness.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
                sqliteDbContext.DbSet<T>().Remove(entity);
                await sqliteDbContext.SaveChangesAsync();
            }
            catch(Exception ex)
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
            return await  sqliteDbContext.DbSet<DailyFoodItem>()
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
            var version = sqliteDbContext.VersionItems.ToList()[0];
            version.Number += 1;
            sqliteDbContext.Update(version);
            sqliteDbContext.SaveChanges();
        }

        public async Task<List<VersionItem>> GetVersionAsync()
        {
            return await sqliteDbContext.VersionItems.ToListAsync();
        }

        public bool HasProfiles()
        {
            return sqliteDbContext.Profiles.Any();
        }

        public async Task<int> Insert<T>(T entity) where T : class
        {
            try
            {
                sqliteDbContext.DbSet<T>().Add(entity);
                await sqliteDbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return 0;
        }

        public async Task<int> Update<T>(T entity) where T : class
        {
            try
            {
                sqliteDbContext.DbSet<T>().Update(entity);
                await sqliteDbContext.SaveChangesAsync();
            }
            catch(Exception ex)
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
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return 0;
        }
    }
}
