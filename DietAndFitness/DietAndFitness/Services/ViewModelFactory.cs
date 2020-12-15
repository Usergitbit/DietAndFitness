using DietAndFitness.Extensions;
using DietAndFitness.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietAndFitness.Services
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly IIocProvider iocProvider;
        public ViewModelFactory(IIocProvider iocProvider)
        {
            this.iocProvider = iocProvider;
        }
        public object Create(Type type, params object[] parameters)
        {
            var vm = iocProvider.Ioc.GetInstanceWithoutCaching(type);
            (vm as IInitializable).Initialize(parameters);
            return vm;
        }
        public async Task<object> CreateAsync(Type type, params object[] parameters)
        {
            var vm = iocProvider.Ioc.GetInstanceWithoutCaching(type);
            await (vm as IInitializable).InitializeAsync(parameters);
            return vm;
        }
    }
}
