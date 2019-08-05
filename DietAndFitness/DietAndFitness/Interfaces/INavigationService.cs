using System.Threading.Tasks;
using Xamarin.Forms;

namespace DietAndFitness.Interfaces
{
    /// <summary>
    /// Interface for implementing navigation methods
    /// </summary>
    public interface INavigationService
    {
        Task NavigateToAsync(Page page);
        Task GoBackAsync();
        Task PushModal(Page page);
        Task PopModal();
        void SetMainPage();
    }
}
