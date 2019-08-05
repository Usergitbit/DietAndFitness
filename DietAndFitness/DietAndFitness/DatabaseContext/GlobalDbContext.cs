using DietAndFitness.Core.EntityFramework.ModelBuilders;
using DietAndFitness.Core.Models;
using DietAndFitness.Interfaces;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;

namespace DietAndFitness.DatabaseContext
{
    public class GlobalDbContext : DbContext
    {
        private const string databaseName = "LocalDatabase.db";
        private string _databasePath;
        public DbSet<GlobalFoodItem> GlobalFoodItems { get; set; }
        public DbSet<VersionItem> VersionItems { get; set; }

        public GlobalDbContext(string databasePath)
        {
            _databasePath = databasePath;
            Database.Migrate();
        }
        public GlobalDbContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            string dbPath = DependencyService.Get<IFilePath>().GetLocalFilePath(databaseName);
            //string dbPath = @"C:\Repositories\DietAndFitness\DietAndFitness\DietAndFitness\Resources\Databases\" + @"\LocalDatabaseEF.db";
            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var globalModelBuilder = new GlobalModelBuilder(modelBuilder);
            globalModelBuilder.CreateGlobalModel();
        }
    }
}
