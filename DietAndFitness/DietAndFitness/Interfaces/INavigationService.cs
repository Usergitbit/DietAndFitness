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
        Task NavigateToAsync(string page, params object[] parameters);
        Task GoBackAsync();
        Task PushModal(string page, params object[] parameters);
        Task PopModal();
        void SetMainPage();
        void Register<TPage>(string pageKey = null);
    }
}
