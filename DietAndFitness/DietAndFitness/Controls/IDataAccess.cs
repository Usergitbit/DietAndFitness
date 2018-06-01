using DietAndFitness.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DietAndFitness.Controls
{
    /// <summary>
    /// Interface for basic CRUD operations
    /// </summary>
    public interface IDataAccess <T> 
    {
        Task<List<T>> Get();
        Task<int> Insert(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(T entity);
    }
}
