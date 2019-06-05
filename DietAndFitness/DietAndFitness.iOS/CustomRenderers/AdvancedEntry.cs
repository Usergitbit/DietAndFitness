using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

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
                Control.WeakDelegate = this;

            }

        }


        [Export("textFieldDidBeginEditing:")]
        public void EditingStarted(UITextField textField)
        {
            textField.PerformSelector(new ObjCRuntime.Selector("selectAll:"), null, 0.0f);
        }

    }
}