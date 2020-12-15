using DietAndFitness.Controls;
using DietAndFitness.Core.EntityFramework.ModelBuilders;
using DietAndFitness.Core.Models;
using DietAndFitness.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace DietAndFitness.DatabaseContext
{
    public class SQLiteDbContext : DbContext
    {
        //private const string databaseName = "LocalDatabaseEF.db";
        private const string databaseName = "LocalFoodItemsDB.db";
        private DbConnection sqliteConnection;

        public DbSet<LocalFoodItem> LocalFoodItems { get; set; }
        public DbSet<DailyFoodItem> DailyFoodItems { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<DietFormula> DietFormulas { get; set; }
        public DbSet<ProfileType> ProfileTypes { get; set; }
        public DbSet<VersionItem> VersionItems { get; set; }

        public SQLiteDbContext()
        {

        }

        public SQLiteDbContext(string dataBasePath)
        {

        }

        protected override async void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            string dbPath = DependencyService.Get<IFileOperations>().GetLocalFilePath(databaseName);
            //string dbPath = @"C:\Repositories\DietAndFitness\DietAndFitness\DietAndFitness\Resources\Databases\" + @"\LocalDatabaseEF.db";
            sqliteConnection = await GlobalSQLiteConnection.GetSQLiteConnection(dbPath);
            optionsBuilder.UseSqlite(sqliteConnection);
            //optionsBuilder.UseSqlite($"Filename={dbPath}");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var sqliteModelBuilder = new SQLiteModelBuilder(modelBuilder);
            sqliteModelBuilder.CreateSQLiteModel();
        }

        public DbSet<T> DbSet<T>() where T : class
        {
            foreach(var prop in GetType().GetProperties())
            {
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GenericTypeArguments[0] == typeof(T))
                    return (DbSet<T>)prop.GetValue(this);
            }
            throw new Exception($"Could not find a property that matches the requested type {typeof(T)} in the class SQLiteDbContext");
        }

        public override void Dispose()
        {
            base.Dispose();
            try
            {
                sqliteConnection.Close();
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Error closing connection! " + ex.Message);
            }

        }

    }
}
