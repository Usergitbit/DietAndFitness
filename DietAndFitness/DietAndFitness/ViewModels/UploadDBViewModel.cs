using DietAndFitness.Core;
using DietAndFitness.Core.Models;
using DietAndFitness.Interfaces;
using Newtonsoft.Json;
using Plugin.FilePicker;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DietAndFitness.ViewModels
{
    public class UploadDBViewModel : ViewModelBase
    {
        public ICommand ExportDataCommand { get; private set; }
        public ICommand ImportDataCommand { get; private set; }
        private readonly IDataAccessService dataService;
        public UploadDBViewModel(INavigationService navigationService, IDataAccessService dataAccessService, IDialogService dialogService) : base(navigationService, dataAccessService, dialogService)
        {
            dataService = dataAccessService;
            ExportDataCommand = new Command(ExportData);
            ImportDataCommand = new Command(ImportData);
        }

        private async void ExportData()
        {

            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                {
                    var result = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
                    if (result[Permission.Storage] != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                    {
                        throw new PermissionsException();
                    }
                }
                var file = await DependencyService.Get<IFileOperations>().CreateFile();
                var fullpath = file.FullPath;
                await dataService.ExportData(file);
                //only ask to share to cloud on mobile devices, windows users can save there directly
                //maybe on ios save to iWhatever and not ask the user at all?
                if (Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.iOS)
                {
                    await Share.RequestAsync(new ShareFileRequest
                    {
                        Title = "Backup to a cloud",
                        File = new ShareFile(fullpath)
                    });
                }
            }
            catch (SelectFileCanceledException)
            {
                //don't bother user if he cancels
            }
            catch (PermissionsException)
            {
                await dialogService.ShowError("Exporting data failed because permissions were not granted.","Error", "OK", null);
            }
            catch (Exception ex)
            {
                await dialogService.ShowError($"{ex.Message} {ex.StackTrace}", "Error exporting data", "OK", null);
            }
        }

        private async void ImportData()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync<StoragePermission>();
                if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                {
                    var result = await CrossPermissions.Current.RequestPermissionAsync<StoragePermission>();
                    if (result != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                    {
                        throw new PermissionsException();
                    }
                }

                var file = await CrossFilePicker.Current.PickFile();
                if (file == null)
                    return; // user canceled file picking

                ICrossFile fileData = new CrossFile(file);
                await dataService.ImportData(fileData);
            }
            catch (PermissionsException)
            {
                await dialogService.ShowError("Exporting data failed because permissions were not granted.", "Error", "OK", null);
            }
            catch (Exception ex)
            {
                await dialogService.ShowError($"{ex.Message} {ex.StackTrace}", "Error importing data", "OK", null);
            }

        }
    }
}
