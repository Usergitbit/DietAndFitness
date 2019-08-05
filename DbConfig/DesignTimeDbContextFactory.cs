using DietAndFitness.DatabaseContext;
using Microsoft.EntityFrameworkCore.Design;
using System.Diagnostics;
using System.IO;

namespace DbConfig
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SQLiteDbContext>
    {
        public SQLiteDbContext CreateDbContext(string[] args)
        {
            Debug.WriteLine(Directory.GetCurrentDirectory() + @"\LocalDatabaseEF.db");

            return new SQLiteDbContext(@"C:\Repositories\DietAndFitness\DietAndFitness\DietAndFitness\Resources\Databases\" + @"\LocalDatabaseEF.db");
        }
    }
}
