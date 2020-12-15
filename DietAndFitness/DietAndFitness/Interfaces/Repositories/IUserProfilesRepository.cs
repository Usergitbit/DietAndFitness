using DietAndFitness.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietAndFitness.Interfaces.Repositories
{
    public interface IUserProfilesRepository : IRepository<Profile, Guid>
    {
        bool HasProfiles();
    }
}
