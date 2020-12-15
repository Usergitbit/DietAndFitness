using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DietAndFitness.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SearchBar), typeof(AdvancedSearchBar))]
namespace DietAndFitness.Droid.CustomRenderers
{
    public class AdvancedSearchBar : SearchBarRenderer
    {
        public AdvancedSearchBar(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                int id = Control.Context.Resources.GetIdentifier("android:id/search_src_text", null, null);
                EditText editText = Control.FindViewById<EditText>(id);
                if (editText != null)
                    editText.SetSelectAllOnFocus(true);
            }
        }
    }
}