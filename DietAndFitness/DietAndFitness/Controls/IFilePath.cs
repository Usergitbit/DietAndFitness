using System;
using System.Collections.Generic;
using System.Text;

namespace DietAndFitness.Controls
{
    /// <summary>
    /// Interface for implementing local file path discovery through dependency
    /// </summary>
    public interface IFilePath
    {
        string GetLocalFilePath(string FileName);
    }
}
