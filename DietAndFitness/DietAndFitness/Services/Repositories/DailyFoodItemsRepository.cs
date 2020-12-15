using DietAndFitness.Core.Models;
using DietAndFitness.Core.Models.Composite;
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
    public class DailyFoodItemsRepository : IDailyFoodItemsRepository
    {
        public int Delete(DailyFoodItem entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(DailyFoodItem entity)
        {
            var context = new SQLiteDbContext();
            context.DailyFoodItems.Remove(entity);
            return context.SaveChangesAsync();
        }

        public DailyFoodItem Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<DailyFoodItem> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Insert(DailyFoodItem entity)
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync(DailyFoodItem entity)
        {
            var context = new SQLiteDbContext();
            context.Attach(entity);
            await context.DailyFoodItems.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public int Update(DailyFoodItem entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(DailyFoodItem entity)
        {
            var context = new SQLiteDbContext();
            context.DailyFoodItems.Update(entity);
            return context.SaveChangesAsync();
        }

        public List<DailyFoodItem> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<DailyFoodItem>> GetAllAsync()
        {
            return sqliteDbContext.DailyFoodItems.AsNoTracking().ToListAsync();
        }

        public Task<List<CompleteFoodItem>> GetCompleteFoodItems(DateTime date)
        {
            return sqliteDbContext.DbSet<DailyFoodItem>()
                .Include(dfi => dfi.FoodItem)
                .Where(dfi => dfi.CreatedAt == date)
                .Select(x => new CompleteFoodItem { DailyFoodItem = x, LocalFoodItem = x.FoodItem })
                .AsNoTracking()
                .ToListAsync();
        }

        public DailyFoodItem Get(Expression<Func<DailyFoodItem, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<DailyFoodItem> GetAsync(Expression<Func<DailyFoodItem, bool>> where)
        {
            return sqliteDbContext.DailyFoodItems.Where(where).Include(dfi => dfi.FoodItem).FirstOrDefaultAsync();
        }

        public List<DailyFoodItem> GetAll(Expression<Func<DailyFoodItem, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<List<DailyFoodItem>> GetAllAsync(Expression<Func<DailyFoodItem, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<List<CompleteFoodItem>> GetCompleteItemAsync(DateTime startDate, DateTime endDate)
        {
            return sqliteDbContext.DailyFoodItems.Include(dfi => dfi.FoodItem)
                .Where(dfi => dfi.CreatedAt >= startDate && dfi.CreatedAt <= endDate)
                .Select(x => new CompleteFoodItem { DailyFoodItem = x, LocalFoodItem = x.FoodItem })
                .AsNoTracking()
                .ToListAsync();
        }

        private static readonly SQLiteDbContext sqliteDbContext = new SQLiteDbContext();

        public DailyFoodItemsRepository()
        {

        }
    }


}
