using DietAndFitness.Core.Models;
using DietAndFitness.Core.Models.Composite;
using DietAndFitness.DatabaseContext;
using DietAndFitness.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DietAndFitness.Interfaces
{
    /// <summary>
    /// Interface for basic CRUD operations
    /// </summary>
    public interface IDataAccessService
    {
        Task ExportData(ICrossFile file);
        Task ImportData(ICrossFile file);

        IDailyFoodItemsRepository DailyFoodItems { get; }
        ILocalFoodItemsRepository LocalFoodItems { get; }
        IUserProfilesRepository UserProfiles { get; }
        IProfileTypesRepository ProfileTypes { get; }
        IDietFormulasRepository DietFormulas { get; }
    }
}
