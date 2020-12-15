using DietAndFitness.Core.Models;
using DietAndFitness.DatabaseContext;
using DietAndFitness.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DietAndFitness.Services.Repositories
{
    public class DietFormulasRepository : IDietFormulasRepository
    {
        public int Delete(DietFormula entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(DietFormula entity)
        {
            throw new NotImplementedException();
        }

        public DietFormula Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public DietFormula Get(Expression<Func<DietFormula, bool>> where)
        {
            throw new NotImplementedException();
        }

        public List<DietFormula> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<DietFormula> GetAll(Expression<Func<DietFormula, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<List<DietFormula>> GetAllAsync()
        {
            return sqliteDbContext.DietFormulas.AsNoTracking().ToListAsync();
        }

        public Task<List<DietFormula>> GetAllAsync(Expression<Func<DietFormula, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<DietFormula> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<DietFormula> GetAsync(Expression<Func<DietFormula, bool>> where)
        {
            throw new NotImplementedException();
        }

        public void Insert(DietFormula entity)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(DietFormula entity)
        {
            throw new NotImplementedException();
        }

        public int Update(DietFormula entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(DietFormula entity)
        {
            throw new NotImplementedException();
        }

        private static readonly SQLiteDbContext sqliteDbContext = new SQLiteDbContext();
        public DietFormulasRepository()
        {

        }
    }
}
