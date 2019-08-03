using DietAndFitness.Core.EntityFramework.ModelBuilders.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietAndFitness.Core.EntityFramework.ModelBuilders
{
    public class SQLiteModelBuilder : BaseModelBuilder
    {
        public SQLiteModelBuilder(ModelBuilder modelBuilder) : base(modelBuilder)
        {

        }

        public void CreateSQLiteModel()
        {
            CreateBaseModel();
        }
    }
}
