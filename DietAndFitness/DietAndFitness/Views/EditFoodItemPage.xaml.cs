﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DietAndFitness
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditFoodItemPage : ContentPage
    {
        public EditFoodItemPage()
        {
            InitializeComponent();
        }

        private async void ModifyFoodButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}