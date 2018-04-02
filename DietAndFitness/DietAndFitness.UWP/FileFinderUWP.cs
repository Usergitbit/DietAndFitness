using DietAndFitness.Model;
using DietAndFitness.UWP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using Windows.Storage;

[assembly: Dependency(typeof(FileFinderUWP))]
namespace DietAndFitness.UWP
{
    //TODO: POSSIBLE REGISTRATION OF DEPENDANCY SERVICE REQUIRED IN APP.XAML.CS FOR RELEASE BUILDS
    public class FileFinderUWP : IFileFinder
    {
        public FileFinderUWP()
        {

        }
        public string GetLocalFilePath(string FileName)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, FileName);
        }
    }
}
