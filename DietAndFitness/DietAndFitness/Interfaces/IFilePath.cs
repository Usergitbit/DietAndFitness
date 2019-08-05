
namespace DietAndFitness.Interfaces
{
    /// <summary>
    /// Interface for implementing local file path discovery through dependency
    /// </summary>
    public interface IFilePath
    {
        string GetLocalFilePath(string FileName);
    }
}
