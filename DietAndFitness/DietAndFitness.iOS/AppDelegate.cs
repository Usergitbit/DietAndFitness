using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Syncfusion.SfChart.XForms.iOS.Renderers;
using Syncfusion.SfGauge.XForms.iOS;
using Syncfusion.SfNumericTextBox.XForms.iOS;
using Syncfusion.SfPicker.XForms.iOS;
using Syncfusion.SfRangeSlider.XForms.iOS;
using Syncfusion.SfSchedule.XForms.iOS;
using UIKit;
using Xamarin;

namespace DietAndFitness.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            SfScheduleRenderer.Init();
            SfRangeSliderRenderer.Init();
            SfNumericTextBoxRenderer.Init();
            SfPickerRenderer.Init();
            SfGaugeRenderer.Init();
            SfChartRenderer.Init();
            Syncfusion.XForms.iOS.PopupLayout.SfPopupLayoutRenderer.Init();
            IQKeyboardManager.SharedManager.Enable = true;
            LoadApplication(new App());
            return base.FinishedLaunching(app, options);
        }
    }
}
