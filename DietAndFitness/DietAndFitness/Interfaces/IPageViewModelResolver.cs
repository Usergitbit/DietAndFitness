using DietAndFitness.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietAndFitness.Interfaces
{
    public interface IPageViewModelResolver
    {
        Type Resolve(Type page);
        void Register<TPage, TViewModel>() where TViewModel : ViewModelBase;
    }
}
