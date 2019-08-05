using System.IO;
using System.Threading.Tasks;

namespace DietAndFitness.Interfaces
{
    public interface IUploadDB
    {
        Task UploadDB(Stream databaseStream, string APIKey);
    }
}
