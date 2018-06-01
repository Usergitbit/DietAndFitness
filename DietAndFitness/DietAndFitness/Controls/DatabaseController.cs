using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
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
                DestinationPath = DependencyService.Get<IFilePath>().GetLocalFilePath(DatabaseName);
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
                
                using (Stream source = Assembly.GetExecutingAssembly().GetManifestResourceStream("DietAndFitness.Resources.Databases." + DatabaseName))
                {
                    if (!File.Exists(DestinationPath))
                        return;
                    else
                    {
                        using (var destination = File.Create(DestinationPath))
                        {
                            source.CopyTo(destination);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine( " Error During Database Copying" + e.Message + e.Source);
            }
        }

    }
}
