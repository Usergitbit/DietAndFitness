using DietAndFitness.Core.Models;
using Microsoft.EntityFrameworkCore;
namespace DietAndFitness.Core.EntityFramework.ModelBuilders.Base
{
    public abstract class BaseModelBuilder
    {
        protected ModelBuilder modelBuilder;
        public BaseModelBuilder(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        protected void CreateBaseModel()
        {
            CreateLocalFoodItem();
            CreateVersionItem();
            CreateProfileTypes();
            CreateDietFormulas();
            CreateProfiles();
            CreateDailyFoodItem();
        }

        private void CreateLocalFoodItem()
        {
            modelBuilder.Entity<LocalFoodItem>()
                .ToTable("LocalFoodItem");

            modelBuilder.Entity<LocalFoodItem>()
                .HasKey(PK => PK.ID);

            modelBuilder.Entity<LocalFoodItem>()
                .Property(NN => NN.Name)
                .IsRequired();

            modelBuilder.Entity<LocalFoodItem>()
                .Property(NN => NN.Calories)
                .IsRequired();

            modelBuilder.Entity<LocalFoodItem>()
                .Property(NN => NN.CreatedAt)
                .IsRequired();

            modelBuilder.Entity<LocalFoodItem>()
                .Property(NN => NN.ModifiedAt)
                .IsRequired();

            modelBuilder.Entity<LocalFoodItem>()
                .HasIndex(I => I.GUID)
                .IsUnique();

            modelBuilder.Entity<LocalFoodItem>()
                .Ignore(IGN => IGN.IsDirty);
        }

        private void CreateVersionItem()
        {
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

        private void CreateProfileTypes()
        {
            modelBuilder.Entity<ProfileType>()
                .HasKey(PK => PK.ID);

            modelBuilder.Entity<ProfileType>()
                .Property(NN => NN.Name)
                .IsRequired();

            modelBuilder.Entity<ProfileType>()
                .Property(NN => NN.CreatedAt)
                .IsRequired();

            modelBuilder.Entity<ProfileType>()
                .HasIndex(I => I.GUID)
                .IsUnique();

            modelBuilder.Entity<ProfileType>()
                .Ignore(IGN => IGN.IsDirty);

        }

        private void CreateDietFormulas()
        {
            modelBuilder.Entity<DietFormula>()
              .HasKey(PK => PK.ID);

            modelBuilder.Entity<DietFormula>()
              .Property(NN => NN.Name)
              .IsRequired();

            modelBuilder.Entity<DietFormula>()
                .Property(NN => NN.CreatedAt)
                .IsRequired();

            modelBuilder.Entity<DietFormula>()
                .Property(NN => NN.ModifiedAt)
                .IsRequired();

            modelBuilder.Entity<DietFormula>()
               .Property(NN => NN.Deleted)
               .IsRequired();

            modelBuilder.Entity<DietFormula>()
                .HasIndex(I => I.GUID)
                .IsUnique();

            modelBuilder.Entity<DietFormula>()
                .Ignore(IGN => IGN.IsDirty);
        }

        private void CreateProfiles()
        {
            modelBuilder.Entity<Profile>()
             .HasKey(PK => PK.ID);

            modelBuilder.Entity<Profile>()
             .Property(NN => NN.Name)
             .IsRequired();

            modelBuilder.Entity<Profile>()
            .Property(NN => NN.CreatedAt)
            .IsRequired();

            modelBuilder.Entity<Profile>()
            .Property(NN => NN.ModifiedAt)
            .IsRequired();

            modelBuilder.Entity<Profile>()
            .Property(NN => NN.Deleted)
            .IsRequired();

            modelBuilder.Entity<Profile>()
            .Property(NN => NN.Height)
            .IsRequired();

            modelBuilder.Entity<Profile>()
            .Property(NN => NN.Weight)
            .IsRequired();

            modelBuilder.Entity<Profile>()
            .Property(NN => NN.BirthDate)
            .IsRequired();

            modelBuilder.Entity<Profile>()
            .Property(NN => NN.Sex)
            .IsRequired();

            modelBuilder.Entity<Profile>()
            .Property(NN => NN.DietFormulaId)
            .IsRequired();

            modelBuilder.Entity<Profile>()
            .Property(NN => NN.ActivityLevel)
            .IsRequired();

            modelBuilder.Entity<Profile>()
            .Property(NN => NN.Name)
            .IsRequired();

            modelBuilder.Entity<Profile>()
            .Property(NN => NN.StartDate)
            .IsRequired();

            modelBuilder.Entity<Profile>()
            .Property(NN => NN.EndDate)
            .IsRequired();

            modelBuilder.Entity<Profile>()
            .Property(NN => NN.ProfileTypesId)
            .IsRequired();

            modelBuilder.Entity<Profile>()
                .HasIndex(I => I.GUID)
                .IsUnique();

            modelBuilder.Entity<Profile>()
                .HasOne(FK => FK.DietFormula)
                .WithMany()
                .HasForeignKey(FK => FK.DietFormulaId)
                .IsRequired();

            modelBuilder.Entity<Profile>()
                .HasOne(FK => FK.ProfileTypes)
                .WithMany()
                .HasForeignKey(FK => FK.ProfileTypesId)
                .IsRequired();

            modelBuilder.Entity<Profile>()
                .Ignore(IGN => IGN.IsDirty);
        }

        private void CreateDailyFoodItem()
        {
            modelBuilder.Entity<DailyFoodItem>()
                .ToTable("DailyFoodItem");

            modelBuilder.Entity<DailyFoodItem>()
                .HasKey(PK => PK.ID);

            modelBuilder.Entity<DailyFoodItem>()
                .Property(NN => NN.Name)
                .IsRequired();

            modelBuilder.Entity<DailyFoodItem>()
                .Property(NN => NN.Quantity)
                .IsRequired();

            modelBuilder.Entity<DailyFoodItem>()
                .Property(NN => NN.CreatedAt)
                .IsRequired();

            modelBuilder.Entity<DailyFoodItem>()
                .Property(NN => NN.ModifiedAt)
                .IsRequired();

            modelBuilder.Entity<DailyFoodItem>()
                .Property(NN => NN.FoodItemID)
                .IsRequired();

            modelBuilder.Entity<DailyFoodItem>()
                .Property(NN => NN.Deleted)
                .IsRequired();

            modelBuilder.Entity<DailyFoodItem>()
                .HasIndex(I => I.GUID)
                .IsUnique();

            modelBuilder.Entity<DailyFoodItem>()
                .HasOne(fk => fk.FoodItem)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<DailyFoodItem>()
                .Ignore(IGN => IGN.IsDirty);
        }

    }
}
