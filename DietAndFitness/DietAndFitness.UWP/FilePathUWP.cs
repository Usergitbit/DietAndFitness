using DietAndFitness.Controls;
using DietAndFitness.Interfaces;
using DietAndFitness.UWP;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(FilePathUWP))]
namespace DietAndFitness.UWP
{
    //TODO: POSSIBLE REGISTRATION OF DEPENDENCY SERVICE REQUIRED IN APP.XAML.CS FOR RELEASE BUILDS
    public class FilePathUWP : IFileOperations
    {
        public FilePathUWP()
        {
            //required
        }

        public string GetLocalFilePath(string FileName)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, FileName);
        }

        public async Task<ICrossFile> CreateFile()
        {
            var savePicker = new Windows.Storage.Pickers.FileSavePicker
            {
                SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Downloads
            };
            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add("Zip", new List<string>() { ".zip" });
            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = $@"DietAndFitnessData{DateTime.Now.ToString("yyyy-MM-dd HH:mm")}";
            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                CachedFileManager.DeferUpdates(file);
                ICrossFile result = new CrossFile(file.Name, file.Path, await file.OpenStreamForWriteAsync(), async () => 
                { 
                    var status = await CachedFileManager.CompleteUpdatesAsync(file);
                    switch(status)
                    {
                        case Windows.Storage.Provider.FileUpdateStatus.Failed:
                        case Windows.Storage.Provider.FileUpdateStatus.Incomplete:
                        case Windows.Storage.Provider.FileUpdateStatus.CurrentlyUnavailable:
                            throw new Exception("Failed to properly update file.");
                        default: break;
                    }
                });
                return result;
            }
            throw new SelectFileCanceledException("File saving canceled");
        }
    }
}
