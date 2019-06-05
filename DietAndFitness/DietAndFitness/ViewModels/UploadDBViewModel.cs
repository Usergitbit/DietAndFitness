using DietAndFitness.Core;
using DietAndFitness.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
            string text = "random text";
            string APIKey = "kety";
            Stream stream = new MemoryStream();
            await DependencyService.Get<IUploadDB>().UploadDB(stream, APIKey);
        }
    }
}
