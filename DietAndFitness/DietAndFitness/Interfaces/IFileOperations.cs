
using Plugin.FilePicker.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DietAndFitness.Interfaces
{
    /// <summary>
    /// Interface for implementing local file path discovery through dependency
    /// </summary>
    public interface IFileOperations
    {
        string GetLocalFilePath(string FileName);
        Task<ICrossFile> CreateFile();
    }

    public class SelectFileCanceledException : Exception
    {

        public SelectFileCanceledException(string message) : base(message)
        {
        }
    }

    public class PermissionsException : Exception
    {
        public PermissionsException()
        {
        }

        public PermissionsException(string message) : base(message)
        {

        }
    }

    /// <summary>
    /// Crossplatform representation of a file. Needed for data exporting becuase uwp API is different for accessing files and needs to do extra stuff
    /// before and after. It is also incompatible with System.IO so the class only has the path and stream.
    /// </summary>
    public class CrossFile : ICrossFile
    {
        private readonly Func<Task> onDispose;
        private readonly FileData fileData;

        public string FullPath => fileData.FilePath;

        public Stream Stream { get => fileData.GetStream(); }
        public string FileName => fileData.FileName;
        

        public CrossFile(string fileName, string fullPath, Stream stream, Func<Task> onDispose = null)
        {
            fileData = new FileData(fullPath, fileName, () => { return stream; });
            this.onDispose = onDispose;
        } 

        public CrossFile(FileData fileData, Func<Task> onDispose = null)
        {
            this.fileData = fileData ?? throw new ArgumentNullException(nameof(fileData));
            this.onDispose = onDispose;
        }

        public async ValueTask DisposeAsync()
        {
            if (onDispose != null)
                await onDispose();
            fileData?.Dispose();
        }
    }
}
