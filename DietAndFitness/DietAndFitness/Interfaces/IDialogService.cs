using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DietAndFitness.Interfaces
{
    /// <summary>
    /// Interface for implementing a Dialog Service
    /// </summary>
    public interface IDialogService
    {
        Task ShowError(string message, string title, string buttonText, Action afterHideCallback);
        Task ShowError(Exception error, string title, string buttonText, Action afterHideCallback);
        Task ShowMessage(string message, string title);
        Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback);
        Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText, Action<bool> afterHideCallback);
        Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText, Func<bool, Task> afterHideCallback);
        Task ShowMessageBox(string message, string title);
    }
}

