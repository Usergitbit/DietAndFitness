using DietAndFitness.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Interface for basic CRUD operations
/// </summary>
namespace DietAndFitness.Controls
{
    public interface IDataAccess <T> where T : DatabaseEntity, new()
    {
        Task<List<T>> Get();
        Task<int> Insert(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(T entity);
    }
}
