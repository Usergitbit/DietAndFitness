using DietAndFitness.Core.Models;
using DietAndFitness.ViewModels.Base;

namespace DietAndFitness.ViewModels
{
    /// <summary>
    /// ViewModel for the AddFoodItemDB page
    /// </summary>
    public class ChangeFoodItemDBViewModel: ChangeBaseViewModel<LocalFoodItem>
    {
        private string progressindicator = "Waiting for input...";
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
        public ChangeFoodItemDBViewModel() : base()
        {
        }
        public ChangeFoodItemDBViewModel(LocalFoodItem selectedItem) : base(selectedItem)
        {
        }

        protected override void OnOperationSuccess()
        {
            ProgressIndicator = "Item added successfully!";
        }

        protected override void OnOperationFailiure()
        {
            ProgressIndicator = "Adding the item failed!";
        }

    }
}
