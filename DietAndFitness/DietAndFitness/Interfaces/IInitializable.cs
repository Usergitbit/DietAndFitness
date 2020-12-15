using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DietAndFitness.Interfaces
{
    public interface IInitializable
    {
        void Initialize(params object[] parameters);
        Task InitializeAsync(params object[] parameters);
    }
}
