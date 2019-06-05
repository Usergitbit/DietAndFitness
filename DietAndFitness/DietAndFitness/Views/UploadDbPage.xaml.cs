using DietAndFitness.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DietAndFitness.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UploadDbPage : ContentPage
	{
        public UploadDBViewModel UploadDBViewModel { get; set; }
        public UploadDbPage ()
		{
			InitializeComponent ();
            UploadDBViewModel = new UploadDBViewModel();
            BindingContext = UploadDBViewModel;
		}
	}
}