using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DietAndFitness.Interfaces
{
    public interface IUploadDB
    {
        Task UploadDB(Stream databaseStream, string APIKey);
    }
}
