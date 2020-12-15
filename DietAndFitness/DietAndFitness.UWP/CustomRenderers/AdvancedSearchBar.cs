using DietAndFitness.UWP.CustomRenderers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
[assembly:ExportRenderer(typeof(SearchBar), typeof(AdvancedSearchBar))]
namespace DietAndFitness.UWP.CustomRenderers
{
    public class AdvancedSearchBar : SearchBarRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if(e.PropertyName == nameof(SearchBar.IsFocused))
            {
                var textboxes = FindVisualChildren<TextBox>(Control);
                if(textboxes != null && textboxes.Any())
                {
                    var autoSuggestTextBox = textboxes.ElementAt(0);
                    autoSuggestTextBox.SelectAll();
                }
            }
        }


        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}
