using DietAndFitness.Core;
using DietAndFitness.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietAndFitness.Services
{
    public class PageViewModelResolver : IPageViewModelResolver
    {
        IDictionary<Type, Type> pageViewModel;
        public PageViewModelResolver()
        {
            pageViewModel = new Dictionary<Type, Type>();
        }
        public void Register<TPage, TViewModel>() where TViewModel : ViewModelBase
        {
            if (!pageViewModel.ContainsKey(typeof(TPage)))
                pageViewModel.Add(typeof(TPage), typeof(TViewModel));
        }

        public Type Resolve(Type page)
        {
            if (!pageViewModel.ContainsKey(page))
                throw new Exception("Page has no registered view model");
            return pageViewModel[page];
        }
    }
}
