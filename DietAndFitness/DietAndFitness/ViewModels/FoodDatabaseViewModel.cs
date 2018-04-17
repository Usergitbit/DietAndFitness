using DietAndFitness.Core;
using DietAndFitness.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using DietAndFitness.Controls;
using System.Windows.Input;
using Xamarin.Forms;
using System.Diagnostics;
using System.ComponentModel;
using DietAndFitness.Validators;
using System.Timers;
using System.Threading.Tasks;
/// <summary>
/// ViewModel for the Food Database Page
/// </summary>
namespace DietAndFitness.ViewModels
{
    public class FoodDatabaseViewModel : ViewModelBase
    {
        public ICommand AddCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand DummyCommand { get; private set; }
        public INavigation Navigation { get; set; }

        #region
        //Navigation should be implemented through a INavigationService that defines a navigation to each type of page
        //Possible need to send VM as parameter to the navigation service call
        //Popup display alert needs to be implemented through service that IDIalogService for possible future disable in settings
        #endregion

        private ObservableCollection<GlobalFoodItem> fooditems;
        private DataAccessLayer<GlobalFoodItem> DBAccess;
        private GlobalFoodItem itemtoadd = new GlobalFoodItem();
        private GlobalFoodItem selecteditem;
        private string progressindicator = "Waiting for input...";

        async Task Navigate()
        {
            //await asdasdasdas.next();
        }
        public GlobalFoodItem SelectedItem
        {
            get
            {
                return selecteditem;
            }
            set
            {
                if (selecteditem == value)
                    return;
                selecteditem = value;
                OnPropertyChanged();
            }
        }
        public string ProgressIndicator
        {
            get
            {
                return progressindicator;
            }

            set
            {
                if (progressindicator == value)
                    return;
                progressindicator = value;
                OnPropertyChanged();
            }
        }

        public GlobalFoodItem ItemToAdd
        {
            get
            {
                return itemtoadd;
            }

            set
            {
                if (value == itemtoadd)
                    return;
                itemtoadd = value;
                OnPropertyChanged();
                
            }
        }
        
            
        public ObservableCollection<GlobalFoodItem> FoodItems
        {
            get
            {
                return fooditems;
            }

            set
            {
                fooditems = value;
                OnPropertyChanged();
             
            }
        }
        public FoodDatabaseViewModel()
        {
            DBAccess = new DataAccessLayer<GlobalFoodItem>(GlobalSQLiteConnection.Database);
            AddCommand = new Command<GlobalFoodItem>(execute: Add, canExecute: ValidateAddButton);
            ItemToAdd.PropertyChanged += OnItemToAddPropertyChanged;
            EditCommand = new Command<GlobalFoodItem>(execute: Edit, canExecute: ValidateEditButton);
            DummyCommand = new Command<GlobalFoodItem>(execute: DummyMethod, canExecute: ValidateNavigationEditButton);
        }

        private bool ValidateNavigationEditButton(GlobalFoodItem Parameter)
        {
            if (SelectedItem == null)
                return false;
            else
                return true;
        }

        private void DummyMethod(object obj)
        {
            //dummy
        }

        private bool ValidateEditButton(GlobalFoodItem arg)
        {
            throw new NotImplementedException();
        }

        private void Edit(GlobalFoodItem obj)
        {
            throw new NotImplementedException();
        }

        private void OnItemToAddPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            (AddCommand as Command).ChangeCanExecute();
        }

        private bool ValidateAddButton(GlobalFoodItem Parameter)
        {
            //try
            //{
            //    //if (Parameter.Name.Equals(String.Empty) || Parameter.Calories.Equals(String.Empty))
            //    if (Parameter.Name != null)
            //        Debug.WriteLine("not null");
            //    else
            //        Debug.WriteLine("null");
            //}
            //catch(Exception e)
            //{
            //    Debug.WriteLine(e.Message + e.Source + e.StackTrace);
            //}

            //if (Parameter == null)

            //{
            //    Debug.WriteLine("i've been here");
            //    return false;
            //}
            //else
            //{
            //    Debug.WriteLine("i am here");
            //    if (Parameter.Name == String.Empty)
            //        return false;
            //}

            return GlobalFoodItemValidator.Check(Parameter);
        }

        public async void LoadList()
        {

            FoodItems = new ObservableCollection<GlobalFoodItem>(await DBAccess.Get());

        }

        public async void Add(GlobalFoodItem Parameter)
        {

            //TODO VALIDATIONS!
            await DBAccess.Insert(Parameter);
            
            ItemToAdd.ResetValues();
            ProgressIndicator = "Item added successfully!";
            await SwitchProgressIndicator();
            //1Debug.WriteLine("ive been executed with parameters" + Parameter.Name + " " + Parameter.Proteins+ " " + Parameter.Brand);
           
        }

        public async Task SwitchProgressIndicator()
        {
            await Task.Delay(2000);
            ProgressIndicator = "Waiting for input...";
        }
      

    }
}
