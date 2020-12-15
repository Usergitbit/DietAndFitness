using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DietAndFitness.Interfaces
{
    public interface IViewModelFactory
    {
        object Create(Type type, params object[] parameters);
        Task<object> CreateAsync(Type type, params object[] parameters);
    }
}
