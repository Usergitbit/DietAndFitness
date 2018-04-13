using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DietAndFitness.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InputPair : ContentView
	{
        public static readonly BindableProperty CaptionProperty = BindableProperty.Create("Caption", typeof(string), typeof(InputPair), string.Empty);
        public static readonly BindableProperty ValueProperty = BindableProperty.Create("Value", typeof(string), typeof(InputPair), string.Empty);

        private static void HandleTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
                ((InputPair)bindable).Value = newValue.ToString();
          
        }

        public static readonly BindableProperty CaptionForeGroundProperty = BindableProperty.Create("CaptionForeGround", typeof(Color), typeof(InputPair), Color.Black);
        public static readonly BindableProperty CaptionBackGroundProperty = BindableProperty.Create("CaptionBackGround", typeof(Color), typeof(InputPair), Color.White);
        public static readonly BindableProperty ValueForeGroundProperty = BindableProperty.Create("ValueForeGround", typeof(Color), typeof(InputPair), Color.Black);
        public static readonly BindableProperty ValueBackGroundProperty = BindableProperty.Create("ValueBackGround", typeof(Color), typeof(InputPair), Color.White);
        
        public string Caption
        {
            get
            {
                return (string)GetValue(CaptionProperty);
            }
            set
            {
                SetValue(CaptionProperty, value);
            }
        }

        public Color CaptionForeGround
        {
            get
            {
                return (Color)GetValue(CaptionForeGroundProperty);
            }
            set
            {
                SetValue(CaptionForeGroundProperty, value);
            }
        }
        public Color CaptionBackGround
        {
            get
            {
                return (Color)GetValue(CaptionBackGroundProperty);
            }
            set
            {
                SetValue(CaptionBackGroundProperty, value);
            }
        }
        public string Value
        {
            get
            {
                return (string)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
            }
        }
  
        public Color ValueForeGround
        {
            get
            {
                return (Color)GetValue(ValueForeGroundProperty);
            }
            set
            {
                SetValue(ValueForeGroundProperty, value);
            }
        }
        public Color ValueBackGround
        {
            get
            {
                return (Color)GetValue(ValueBackGroundProperty);
            }
            set
            {
                SetValue(ValueForeGroundProperty, value);
            }
        }
		public InputPair ()
		{
			InitializeComponent ();
		}
	}
}