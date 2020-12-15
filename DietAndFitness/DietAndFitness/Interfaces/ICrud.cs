using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DietAndFitness.Interfaces
{
    public interface ICrud<TEntity, TKey>
    {
        List<TEntity> GetAll();
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> where);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> where);
        TEntity Get(TKey id);
        Task<TEntity> GetAsync(TKey id);
        TEntity Get(Expression<Func<TEntity,bool>> where);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where);
        void Insert(TEntity entity);
        Task InsertAsync(TEntity entity);
        int Update(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        int Delete(TEntity entity);
        Task<int> DeleteAsync(TEntity entity);
    }
}
