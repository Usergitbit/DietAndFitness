using DietAndFitness.Core.Models;
using DietAndFitness.ViewModels.Base;

namespace DietAndFitness.ViewModels
{
    /// <summary>
    /// ViewModel for the AddFoodItemDB page
    /// </summary>
    public class ChangeFoodItemDBViewModel: ChangeBaseViewModel<LocalFoodItem>
    {
        public ChangeFoodItemDBViewModel() : base()
        {
        }
        public ChangeFoodItemDBViewModel(LocalFoodItem selectedItem) : base(selectedItem)
        {
        }

    }
}
