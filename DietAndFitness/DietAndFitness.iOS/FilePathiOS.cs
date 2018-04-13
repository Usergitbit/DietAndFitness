using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DietAndFitness.Controls;
using DietAndFitness.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(FilePathiOS))]
namespace DietAndFitness.iOS
{
    public class FilePathiOS : IFilePath
    {
        public FilePathiOS()
        {
            //required
        }
        public string GetLocalFilePath(string FileName)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, FileName);
        }
    }
}