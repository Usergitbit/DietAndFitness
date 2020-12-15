using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DietAndFitness.Controls;
using DietAndFitness.Droid;
using DietAndFitness.Interfaces;
using Java.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(FilePathAndroid))]
namespace DietAndFitness.Droid
{
    public class FilePathAndroid : IFileOperations
    {
        public FilePathAndroid()
        {

        }
        public Task<ICrossFile> CreateFile()
        {
            var fileName = $@"DietAndFitnessData{DateTime.Now.ToString("yyyy-MM-dd HH_mm")}.zip";

            //new shit api can only save in the app folder jesus fucking christ
            var newPath = Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDownloads).AbsolutePath;

            //deprecated but the only thing that gives us the download folder???
            //var oldPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).AbsolutePath;
            var randomPath = Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDownloads);
            var path = Path.Combine(newPath, fileName);

            ICrossFile result = new CrossFile(fileName, path, System.IO.File.Create(path));
            return Task.FromResult(result);
        }

        public string GetLocalFilePath(string FileName)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, FileName);
        }
    }
}