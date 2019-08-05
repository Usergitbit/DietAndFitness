using DietAndFitness.Core;
using System.Windows.Input;
using Xamarin.Forms;

namespace DietAndFitness.ViewModels
{
    public class UploadDBViewModel : ViewModelBase
    {
        public ICommand UploadCommand { get; private set; }
        public UploadDBViewModel() : base()
        {
            UploadCommand = new Command(execute: UploadDb);
        }

        private async void UploadDb()
        {
            await dialogService.ShowError("Not implemented yet.", "Error", "OK", null);
        }
    }
}
