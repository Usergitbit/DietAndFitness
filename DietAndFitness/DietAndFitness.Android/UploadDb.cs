using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DietAndFitness.Interfaces;
using Xamarin.Forms;

namespace DietAndFitness.Droid
{
    public class UploadDb : IUploadDB
    {
        public UploadDb()
        {

        }

        public Task UploadDB(Stream databaseStream, string APIKey)
        {
            throw new NotImplementedException();
        }
    }
}