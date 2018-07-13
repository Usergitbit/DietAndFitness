using Syncfusion.SfRangeSlider.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DietAndFitness.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateUserProfilePage : ContentPage
	{
		public CreateUserProfilePage ()
		{
            
			InitializeComponent ();

		}

        private void OnFormulaPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (formulaPicker.SelectedIndex == 1)
            {
                bodyFatNameLabel.IsVisible = true;
                bodyFatEntry.IsVisible = true;
            }
            else
            {
                bodyFatNameLabel.IsVisible = false;
                bodyFatEntry.IsVisible = false;
            }
        }


    }
}