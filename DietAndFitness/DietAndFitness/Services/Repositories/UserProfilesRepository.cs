using DietAndFitness.Core.Models;
using DietAndFitness.DatabaseContext;
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
    public class UserProfilesRepository : IUserProfilesRepository
    {
        public int Delete(Profile entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(Profile entity)
        {
            throw new NotImplementedException();
        }

        public Profile Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Profile Get(Expression<Func<Profile, bool>> where)
        {
            throw new NotImplementedException();
        }

        public List<Profile> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<Profile>> GetAllAsync()
        {
            return sqliteDbContext.Profiles.AsNoTracking().ToListAsync();
        }

        public Task<Profile> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Profile entity)
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync(Profile entity)
        {
            var context = new SQLiteDbContext();
            context.Attach(entity);
            await context.Profiles.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public int Update(Profile entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Profile entity)
        {
            var context = new SQLiteDbContext();
            context.Profiles.Update(entity);
            return context.SaveChangesAsync();
        }

        public Task<Profile> GetAsync(Expression<Func<Profile, bool>> where)
        {
            return sqliteDbContext.Profiles.Include(p => p.DietFormula).Where(where).FirstOrDefaultAsync();
        }

        public List<Profile> GetAll(Expression<Func<Profile, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<List<Profile>> GetAllAsync(Expression<Func<Profile, bool>> where)
        {
            throw new NotImplementedException();
        }

        public bool HasProfiles()
        {
            if (!sqliteDbContext.Profiles.Any())
                return false;

            //will throw exception if Profiles is empty
            var profiles = sqliteDbContext.Profiles.AsNoTracking().ToList();
            var foundProfile = profiles.Where(p => p.StartDate <= DateTime.Today && p.EndDate >= DateTime.Today).Any();
            return foundProfile;
        }

        private static readonly SQLiteDbContext sqliteDbContext = new SQLiteDbContext();
        public UserProfilesRepository()
        {

        }
    }
}
