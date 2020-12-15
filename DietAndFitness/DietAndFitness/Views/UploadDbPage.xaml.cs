using DietAndFitness.ViewModels;
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
            BindingContext = UploadDBViewModel;
		}
	}
}