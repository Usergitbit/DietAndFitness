using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(Entry), typeof(DietAndFitness.UWP.CustomRenderers.AdvancedEntry))]
namespace DietAndFitness.UWP.CustomRenderers
{
    /// <summary>
    /// Entry that selects all text when focused.
    /// </summary>
    public class AdvancedEntry : EntryRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == nameof(Entry.IsFocused)
                && Control != null && Control.FocusState != Windows.UI.Xaml.FocusState.Unfocused)
                Control.SelectAll();
        }
    }
}

