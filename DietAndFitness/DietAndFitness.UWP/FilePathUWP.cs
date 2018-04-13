using DietAndFitness.Controls;
using DietAndFitness.UWP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(FilePathUWP))]
namespace DietAndFitness.UWP
{
    //TODO: POSSIBLE REGISTRATION OF DEPENDENCY SERVICE REQUIRED IN APP.XAML.CS FOR RELEASE BUILDS
    public class FilePathUWP : IFilePath
    {
        public FilePathUWP()
        {
            //required
        }
        public string GetLocalFilePath(string FileName)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, FileName);
        }
    }
}
