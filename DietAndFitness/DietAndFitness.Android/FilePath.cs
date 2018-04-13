using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DietAndFitness.Controls;
using DietAndFitness.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(FilePathAndroid))]
namespace DietAndFitness.Droid
{
    public class FilePathAndroid : IFilePath
    {
        public FilePathAndroid()
        {

        }
        public string GetLocalFilePath(string FileName)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, FileName);
        }
    }
}