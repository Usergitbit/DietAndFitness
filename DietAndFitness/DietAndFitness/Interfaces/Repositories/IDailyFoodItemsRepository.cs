using DietAndFitness.Core.Models;
using DietAndFitness.Core.Models.Composite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DietAndFitness.Interfaces.Repositories
{
    public interface IDailyFoodItemsRepository : IRepository<DailyFoodItem, Guid>
    {
        Task<List<CompleteFoodItem>> GetCompleteFoodItems(DateTime date);
        Task<List<CompleteFoodItem>> GetCompleteItemAsync(DateTime startDate, DateTime endDate);
    }
}
