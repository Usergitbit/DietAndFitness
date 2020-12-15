using DietAndFitness.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DietAndFitness.Interfaces.Repositories
{
    public interface ILocalFoodItemsRepository : IRepository<LocalFoodItem, Guid>
    {
        Task<List<LocalFoodItem>> GetRecentlyAddedItems();
    }
}
