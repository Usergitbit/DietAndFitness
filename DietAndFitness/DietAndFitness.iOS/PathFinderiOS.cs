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

[assembly: Dependency(typeof(PathFinderiOS))]
namespace DietAndFitness.iOS
{
    class PathFinderiOS : IPathFinder
    {
        public PathFinderiOS()
        {

        }
        public string GetLocalPath()
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return libFolder;
        }
    }
}