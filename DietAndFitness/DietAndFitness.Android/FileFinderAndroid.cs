using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DietAndFitness.Droid;
using DietAndFitness.Model;
using Xamarin.Forms;


[assembly: Dependency(typeof(FileFinderAndroid))]
namespace DietAndFitness.Droid
{
    public class FileFinderAndroid : IFileFinder
    {
        public FileFinderAndroid()
        {

        }
        public string GetLocalFilePath(string FileName)
        {
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, FileName);
        }
    }
}