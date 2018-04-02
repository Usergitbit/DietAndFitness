using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DietAndFitness.iOS;
using DietAndFitness.Model;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileFinderiOS))]
namespace DietAndFitness.iOS
{
    public class FileFinderiOS : IFileFinder
    {
        public FileFinderiOS()
        {

        }
        public string GetLocalFilePath(string FileName)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if(!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, FileName);
        }
    }
}