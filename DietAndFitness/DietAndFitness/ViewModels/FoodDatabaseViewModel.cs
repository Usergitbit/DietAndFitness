using System.Threading.Tasks;
using DietAndFitness.Views;
using DietAndFitness.ViewModels.Base;
using DietAndFitness.Core.Models;

namespace DietAndFitness.ViewModels
{
    /// <summary>
    /// ViewModel for the Food Database page
    /// </summary>
    public class FoodDatabaseViewModel : ListBaseViewModel<LocalFoodItem, ChangeFoodItemDB>
    {
        #region Members
        private string progressindicator = "Waiting for input...";
        #endregion
        #region Properties
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


        #endregion
        public FoodDatabaseViewModel() : base()
        {

        }
        #region Methods
        public async Task SwitchProgressIndicator()
        {
            await Task.Delay(2000);
            ProgressIndicator = "Waiting for input...";
        }
        #endregion
    }
}
