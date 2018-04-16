using DietAndFitness.Controls;
using DietAndFitness.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace DietAndFitness
{
	public partial class App : Application
	{
        private string LocalDatabase = "LocalDatabase.db";
		public App ()
		{
                InitializeComponent();
                GlobalDatabaseController DBControl = new GlobalDatabaseController(LocalDatabase);
                DBControl.CopyDatabase();
                GlobalSQLiteConnection.ConnectToDatabaseAsync(DBControl.DestinationPath);
                MainPage = new HomePage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
