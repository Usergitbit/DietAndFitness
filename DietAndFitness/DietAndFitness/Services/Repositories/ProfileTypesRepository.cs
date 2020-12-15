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
    public class ProfileTypesRepository : IProfileTypesRepository
    {
        public int Delete(ProfileType entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(ProfileType entity)
        {
            throw new NotImplementedException();
        }

        public ProfileType Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public ProfileType Get(Expression<Func<ProfileType, bool>> where)
        {
            throw new NotImplementedException();
        }

        public List<ProfileType> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<ProfileType> GetAll(Expression<Func<ProfileType, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProfileType>> GetAllAsync()
        {
            return sqliteDbContext.ProfileTypes.AsNoTracking().ToListAsync();
        }

        public Task<List<ProfileType>> GetAllAsync(Expression<Func<ProfileType, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<ProfileType> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ProfileType> GetAsync(Expression<Func<ProfileType, bool>> where)
        {
            throw new NotImplementedException();
        }

        public void Insert(ProfileType entity)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(ProfileType entity)
        {
            throw new NotImplementedException();
        }

        public int Update(ProfileType entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(ProfileType entity)
        {
            throw new NotImplementedException();
        }
        private static readonly SQLiteDbContext sqliteDbContext = new SQLiteDbContext();
        public ProfileTypesRepository()
        {

        }
    }
}
