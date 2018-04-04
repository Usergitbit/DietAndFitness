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
using DietAndFitness.Droid;
using DietAndFitness.Model;
using Xamarin.Forms;

[assembly: Dependency(typeof(PathFinderAndroid))]
namespace DietAndFitness.Droid
{
    public class PathFinderAndroid : IPathFinder
    {
        public PathFinderAndroid()
        {

        }
        public string GetLocalPath()
        {
            return System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        }
    }
}