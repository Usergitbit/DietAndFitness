using DietAndFitness.Core.Models;
using DietAndFitness.DatabaseContext;
using DietAndFitness.Interfaces;
using DietAndFitness.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DietAndFitness.Services.Repositories
{
    public class LocalFoodItemsRepository : ILocalFoodItemsRepository
    {
        public int Delete(LocalFoodItem entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(LocalFoodItem entity)
        {
            var context = new SQLiteDbContext();
            context.LocalFoodItems.Remove(entity);
            return context.SaveChangesAsync();
        }

        public LocalFoodItem Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<LocalFoodItem> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<LocalFoodItem>> GetAllAsync()
        {
            return sqliteDbContext.LocalFoodItems.AsNoTracking().ToListAsync();
        }

        public Task<LocalFoodItem> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Insert(LocalFoodItem entity)
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync(LocalFoodItem entity)
        {
            var context = new SQLiteDbContext();
            await context.LocalFoodItems.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public int Update(LocalFoodItem entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(LocalFoodItem entity)
        {
            var context = new SQLiteDbContext();
            context.LocalFoodItems.Update(entity);
            return context.SaveChangesAsync();
        }

        public async Task<List<LocalFoodItem>> GetRecentlyAddedItems()
        {
            var items = await sqliteDbContext.DailyFoodItems.Include(dfi => dfi.FoodItem)
                .Where(dfi => dfi.CreatedAt <= DateTime.Today && dfi.CreatedAt >= DateTime.Today.AddDays(-7))
                .AsNoTracking()
                .ToListAsync();

            var groupedItems = items.GroupBy(x => x.FoodItemID)
                .Where(x => x.Count() > 1).Select(x => x.First().FoodItem)
                .ToList();
                       
            return groupedItems;
        }

        public LocalFoodItem Get(Expression<Func<LocalFoodItem, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<LocalFoodItem> GetAsync(Expression<Func<LocalFoodItem, bool>> where)
        {
            return sqliteDbContext.LocalFoodItems.Where(where).FirstOrDefaultAsync();
        }

        public List<LocalFoodItem> GetAll(Expression<Func<LocalFoodItem, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<List<LocalFoodItem>> GetAllAsync(Expression<Func<LocalFoodItem, bool>> where)
        {
            throw new NotImplementedException();
        }

        private static readonly SQLiteDbContext sqliteDbContext = new SQLiteDbContext();

        public LocalFoodItemsRepository()
        {

        }
    }
}
