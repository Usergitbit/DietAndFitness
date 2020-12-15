using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DietAndFitness.Interfaces
{
    public interface ICrossFile : IAsyncDisposable
    {
        string FullPath { get; }
        Stream Stream { get; }
        string FileName { get; }

    }
}
