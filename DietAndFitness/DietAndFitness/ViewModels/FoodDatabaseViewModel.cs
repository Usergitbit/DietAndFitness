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
using DietAndFitness.Services;
/// <summary>
/// ViewModel for the Food Database Page
/// </summary>
namespace DietAndFitness.ViewModels
{
    public class FoodDatabaseViewModel : ViewModelBase
    {
        public ICommand AddCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand PlaceHolderNavigationCommand { get; private set; }
        public INavigation Navigation { get; set; }
        private readonly IDialogService _dialogService;

        #region TODO
        //Navigation should be implemented through a INavigationService that defines a navigation to each type of page
        //Possible need to send VM as parameter to the navigation service call
        //Popup display alert needs to be implemented through service that IDIalogService for possible future disable in settings
        //CLEAN items when navigating to a new page ?
        #endregion

        private ObservableCollection<GlobalFoodItem> fooditems;
        private DataAccessLayer<GlobalFoodItem> DBAccess;
        private string progressindicator = "Waiting for input...";
        private GlobalFoodItem selectedItem;
        private GlobalFoodItem itemToAdd;
        private RelayCommand _confirmCommand;
        async Task Navigate()
        {
            //await asdasdasdas.next();
        }
        public GlobalFoodItem SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                if (selectedItem == value)
                    return;
                selectedItem = value;
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
                return itemToAdd;
            }

            set
            {
                if (value == itemToAdd)
                    return;
                itemToAdd = value;
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
            _dialogService = new DialogService();
            ItemToAdd = new GlobalFoodItem();
            SelectedItem = new GlobalFoodItem();
            DBAccess = new DataAccessLayer<GlobalFoodItem>(GlobalSQLiteConnection.Database);
            AddCommand = new Command<GlobalFoodItem>(execute: Add, canExecute: ValidateAddButton);
            EditCommand = new Command<GlobalFoodItem>(execute: Edit, canExecute: ValidateEditButton);
            PlaceHolderNavigationCommand = new Command(execute: PlaceHolderNavigationFunction);
            ItemToAdd.PropertyChanged += OnItemToAddPropertyChanged;
            
        }

        private bool ValidateEditButton(GlobalFoodItem arg)
        {
            if (arg == null)
                Debug.WriteLine("parameter EDIT was null");
            // return false;
            else
            {
                Debug.WriteLine("I got here");
                if (arg.IsDirty == true)
                {
                    Debug.WriteLine("Item is dirty");
                    return true;
                }
                else
                {
                    Debug.WriteLine("Item is not dirty");
                    return false;
                }
            }
            return false;
        }

        private void PlaceHolderNavigationFunction(object obj)
        {

        }

        public void OnItemToEditPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Debug.WriteLine("I changed the can execute for edit");
            (EditCommand as Command).ChangeCanExecute();

        }
        private void OnItemToAddPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Debug.WriteLine("I cahnged the can execute for add ");
            (AddCommand as Command).ChangeCanExecute();
        }

        public void BindSelectedItem()
        {
            SelectedItem.PropertyChanged += OnItemToEditPropertyChanged;
        }
        

        private async void Edit(GlobalFoodItem parameter)
        {
            if (parameter != null)
            {
                await DBAccess.Update(parameter);
                Debug.WriteLine(await DBAccess.Update(parameter));
                ProgressIndicator = "Item edited successfully!";
                await SwitchProgressIndicator();
            }
            else
                Debug.WriteLine("parameter was null");

        }

      

        private bool ValidateAddButton(GlobalFoodItem Parameter)
        {
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
        public RelayCommand ConfirmCommand
        {
            get
            {
                return _confirmCommand
                       ?? (_confirmCommand = new RelayCommand(
                           async () =>
                           {
                               await _dialogService.ShowMessage("Are you sure you want to delete this item?",
                                  "Warning!",
                                  "Yes",
                                  "No",
                                   async (r) => { if (r == true)
                                                    {
                                                        await DBAccess.Delete(SelectedItem);
                                                        LoadList();
                                                    }
                                                 });
                           }));
            }
        }
        #region Command Dialog Service Example 
        //public RelayCommand ConfirmCommand
        //{
        //    get
        //    {
        //        return _confirmCommand
        //               ?? (_confirmCommand = new RelayCommand(
        //                   async () =>
        //                   {
        //                       await _dialogService.ShowMessage("Does dialog callback work?",
        //                          "Callback Test",
        //                          "Yup",
        //                          "Nope",
        //                           (r) => { _dialogService.ShowMessage("Result: " + r.ToString(), "Result", "OK", null); });
        //                   }));
        //    }
        //}
        #endregion
    }
}
