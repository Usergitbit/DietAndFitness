using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DietAndFitness.Controls;
using DietAndFitness.Interfaces;
using DietAndFitness.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(FilePathiOS))]
namespace DietAndFitness.iOS
{
    public class FilePathiOS : IFileOperations
    {
        public FilePathiOS()
        {
            //required
        }

        public Task<ICrossFile> CreateFile()
        {
            throw new NotImplementedException();
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