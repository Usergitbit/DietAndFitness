using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// Interface for implementing local file path discovery through dependency
/// </summary>
namespace DietAndFitness.Controls
{
    public interface IFilePath
    {
        string GetLocalFilePath(string FileName);
    }
}
