using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Entry), typeof(DietAndFitness.iOS.CustomRenderers.AdvancedEntry))]
namespace DietAndFitness.iOS.CustomRenderers
{
    public class AdvancedEntry : EntryRenderer, IUITextFieldDelegate
    {
        public AdvancedEntry()
        {


        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                var nativeTextField = Control;
                nativeTextField.EditingDidBegin += (object sender, EventArgs eIos) =>
                {
                    nativeTextField.PerformSelector(new ObjCRuntime.Selector("selectAll"), null, 0.0f);
                };
            }
        }

    }
}