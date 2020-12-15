using DietAndFitness.Core;
using DietAndFitness.Extensions;
using DietAndFitness.Interfaces;
using DietAndFitness.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DietAndFitness.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : MasterDetailPage
    {
        //private readonly Dictionary<Type, NavigationPage> menuPages = new Dictionary<Type, NavigationPage>();
        public HomePage()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (!(e.SelectedItem is HomePageMenuItem item))
                return;

            MasterPage.LoadingLabel.IsVisible = true;
            await Task.Delay(1);

            //if(!menuPages.ContainsKey(item.TargetType))
            //{
                var page = (Page)FastActivator.GetActivator(item.TargetType)();
                ViewModelBase vm = null;
                await Task.Run(() =>
                {
                    var vmType = App.Ioc.GetInstance<IPageViewModelResolver>().Resolve(item.TargetType);
                    vm = (ViewModelBase)App.Ioc.GetInstance<IViewModelFactory>().Create(vmType);
                });
                page.BindingContext = vm;
            //menuPages.Add(item.TargetType, new NavigationPage(page));
            //}

            Detail = new NavigationPage(page); //menuPages[item.TargetType];


            if (Device.RuntimePlatform == Device.Android)
                await Task.Delay(100);

            MasterPage.ListView.SelectedItem = null;
            MasterPage.LoadingLabel.IsVisible = false;
            IsPresented = false;
        }
    }
}