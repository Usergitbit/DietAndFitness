//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Diagnostics;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Input;
//using DietAndFitness.Controls;
//using DietAndFitness.Core;
//using DietAndFitness.Entities;
//using DietAndFitness.Services;
//using Xamarin.Forms;

//namespace DietAndFitness.ViewModels
//{
//    /// <summary>
//    /// ViewModel for the EditFoodItemDB page
//    /// </summary>
//    public class EditFoodItemDBViewModel<T> : ViewModelBase where T: DatabaseEntity, new()
//    {
//        #region Members
//        private T itemToEdit;
//        private readonly IDialogService dialogService;
//        private string progressindicator = "Waiting for input...";
//        private DataAccessLayer DBLocalAccess;
//        #endregion
//        #region Properties
//        public T ItemToEdit
//        {
//            get
//            {
//                return itemToEdit;
//            }

//            set
//            {
//                if (value == itemToEdit)
//                    return;
//                itemToEdit = value;
//                OnPropertyChanged();

//            }
//        }
//        public string ProgressIndicator
//        {
//            get
//            {
//                return progressindicator;
//            }

//            set
//            {
//                if (progressindicator == value)
//                    return;
//                progressindicator = value;
//                OnPropertyChanged();
//            }
//        }

//        public ICommand CloseCommand { get; private set; }
//        public ICommand ConfirmCommand { get; private set; }
//        #endregion
//        public EditFoodItemDBViewModel(DatabaseEntity selectedItem, NavigationService navigationService) : base(navigationService)
//        {
//            dialogService = new DialogService();
//            itemToEdit = (T)selectedItem;
//            DBLocalAccess = new DataAccessLayer(GlobalSQLiteConnection.LocalDatabase);
//            CloseCommand = new Command(execute: Close);
//            ConfirmCommand = new Command<T>(execute: Edit, canExecute: ValidateConfirmButton);
//            ItemToEdit.PropertyChanged += OnItemToEditPropertyChanged;
//        }
//        #region Methods
//        private void OnItemToEditPropertyChanged(object sender, PropertyChangedEventArgs e)
//        {
//            if(ItemToEdit != null)
//                (ConfirmCommand as Command).ChangeCanExecute();
//        }

//        private async void Close()
//        {
//            await navigationService.PopModal();
//        }

//        private async void Edit(T parameter)
//        {
//            if (parameter != null)
//            {
//                try
//                {
//                    await DBLocalAccess.Update(parameter);
//                    parameter.Clean();
//                    Debug.WriteLine(await DBLocalAccess.Update(parameter));
//                    ProgressIndicator = "Item edited successfully!";
//                    OnItemToEditPropertyChanged(null, null);
//                }
//                catch(Exception ex)
//                {
//                    await dialogService.ShowError(ex, "Error", "Ok", null);
//                }
//            }
//            else
//                Debug.WriteLine("parameter was null");
//        }
//        private bool ValidateConfirmButton(T parameter)
//        {
//            if (parameter == null)
//                Debug.WriteLine("parameter EDIT was null");
//            else
//            {
//                Debug.WriteLine("I got here");
//                if (parameter.IsDirty == true)
//                {
//                    Debug.WriteLine("Item is dirty");
//                    if (parameter.IsValid())
//                        return true;
//                    else
//                        return false;
//                }
//                else
//                {
//                    Debug.WriteLine("Item is not dirty");
//                    return false;
//                }
//            }
//            return false;
//        }
//        #endregion
//    }
//}
