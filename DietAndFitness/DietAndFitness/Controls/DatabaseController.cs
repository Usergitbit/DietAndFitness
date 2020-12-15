using DietAndFitness.Interfaces;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Xamarin.Forms;
namespace DietAndFitness.Controls
{
    /// <summary>
    /// Class that controls the local copy of the database provided with the app
    /// </summary>
    public class DatabaseController
    {
        private string DatabaseName { get; set; }
        public string DestinationPath { get; set; }

        public DatabaseController (string _DatabaseName)
        {
            DatabaseName = _DatabaseName;
            try
            {
                DestinationPath = DependencyService.Get<IFileOperations>().GetLocalFilePath(DatabaseName);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error at getting file Path" + e.InnerException + e.StackTrace );
            }
        }

        public void CopyDatabase()
        {
            try
            {
                if (File.Exists(DestinationPath))
                    return;
                using Stream source = Assembly.GetExecutingAssembly().GetManifestResourceStream("DietAndFitness.Resources.Databases." + DatabaseName);
                using var destination = File.Create(DestinationPath);
                source.CopyTo(destination);

            }
            catch (Exception e)
            {
                Debug.WriteLine( " Error During Database Copying" + e.Message + e.Source);
            }
        }

    }
}
