using DietAndFitness.Core;
using DietAndFitness.Extensions;
using DietAndFitness.Interfaces;
using DietAndFitness.ViewModels;
using DietAndFitness.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DietAndFitness.Services
{
    public class NavigationService : INavigationService
    {
        private readonly Dictionary<string, Type> pages;

        private readonly IViewModelFactory viewModelFactory;
        private readonly IPageViewModelResolver pageViewModelResolver;

        public NavigationService(IViewModelFactory viewModelFactory, IPageViewModelResolver pageViewModelResolver)
        {
            pages = new Dictionary<string, Type>();
            this.viewModelFactory = viewModelFactory;
            this.pageViewModelResolver = pageViewModelResolver;
        }
        public Task GoBackAsync()
        {
            throw new NotImplementedException();
        }

        public Task NavigateToAsync(Page page)
        {
            throw new NotImplementedException();
        }

        public Task NavigateToAsync(string page, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public async Task PopModal()
        {
            await ((App.Current.MainPage as HomePage).Detail as NavigationPage).PopAsync();
        }

        public Task PushModal(string page)
        {
            throw new NotImplementedException();
        }

        public async Task PushModal(string page, params object[] parameters)
        {
            var pageType = pages[page];
            ViewModelBase vm = null;
            var task = Task.Run(async () =>
            {
                vm = (ViewModelBase)await viewModelFactory.CreateAsync(pageViewModelResolver.Resolve(pageType), parameters);
            });
            var pageObject = (Page)FastActivator.GetActivator(pageType)();
            await task;
            pageObject.BindingContext = vm;
            await ((App.Current.MainPage as HomePage).Detail as NavigationPage).PushAsync(pageObject);
        }

        public void Register<TPage>(string pageKey = null)
        {
            if (!pages.ContainsKey(pageKey ?? typeof(TPage).Name))
                pages.Add(pageKey ?? typeof(TPage).Name, typeof(TPage));
            else
                throw new Exception("Page was "+ typeof(TPage) + " regsitered twice.");
               
        }

        public void SetMainPage()
        {
            var daily = new DailyFoodListPage();
            var navigationPage = new NavigationPage(daily);
            var vm = viewModelFactory.Create(typeof(DailyFoodListViewModel));
            daily.BindingContext = vm;
            var homePage = new HomePage
            {
                Detail = navigationPage
            };
            Application.Current.MainPage = homePage;
        }


    }
}
