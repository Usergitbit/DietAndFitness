using DietAndFitness.Core.EntityFramework.ModelBuilders.Base;
using DietAndFitness.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietAndFitness.Core.EntityFramework.ModelBuilders
{
    public class GlobalModelBuilder : BaseModelBuilder
    {
        public GlobalModelBuilder(ModelBuilder modelBuilder) : base(modelBuilder)
        {

        }

        public void CreateGlobalModel()
        {
            modelBuilder.Entity<GlobalFoodItem>()
                .ToTable("GlobalFoodItem");

            modelBuilder.Entity<GlobalFoodItem>()
                .HasKey(PK => PK.ID);

            modelBuilder.Entity<GlobalFoodItem>()
                .Property(NN => NN.Name)
                .IsRequired();

            modelBuilder.Entity<GlobalFoodItem>()
                .Property(NN => NN.Calories)
                .IsRequired();

            modelBuilder.Entity<GlobalFoodItem>()
                .Property(NN => NN.CreatedAt)
                .IsRequired();

            modelBuilder.Entity<GlobalFoodItem>()
                .Property(NN => NN.ModifiedAt)
                .IsRequired();

            modelBuilder.Entity<GlobalFoodItem>()
                .HasIndex(I => I.GUID)
                .IsUnique();

            modelBuilder.Entity<GlobalFoodItem>()
                .Ignore(IGN => IGN.IsDirty);

            modelBuilder.Entity<VersionItem>()
                .ToTable("VersionItem");

            modelBuilder.Entity<VersionItem>()
                .HasKey(PK => PK.Number);

            modelBuilder.Entity<VersionItem>()
                .Ignore(IGN => IGN.IsDirty);

            modelBuilder.Entity<VersionItem>()
                .Ignore(IGN => IGN.CreatedAt);

            modelBuilder.Entity<VersionItem>()
                .Ignore(IGN => IGN.Deleted);

            modelBuilder.Entity<VersionItem>()
                .Ignore(IGN => IGN.ID);

            modelBuilder.Entity<VersionItem>()
                .Ignore(IGN => IGN.GUID);

            modelBuilder.Entity<VersionItem>()
                .Ignore(IGN => IGN.ModifiedAt);

            modelBuilder.Entity<VersionItem>()
               .Ignore(IGN => IGN.Name);

        }
    }
}
