using Syncfusion.SfPicker.XForms.UWP;
using Syncfusion.SfGauge.XForms.UWP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace DietAndFitness.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            SfPickerRenderer.Init();
            new Syncfusion.SfGauge.XForms.UWP.SfGaugeRenderer();
            LoadApplication(new DietAndFitness.App());
        }
    }
}
