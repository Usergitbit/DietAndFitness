using DietAndFitness.Model;
using DietAndFitness.UWP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(PathFinderUWP))]
namespace DietAndFitness.UWP
{
    public class PathFinderUWP : IPathFinder
    {
        public PathFinderUWP()
        {

        }
        public string GetLocalPath()
        {
            return ApplicationData.Current.LocalFolder.Path;
        }
    }
}
