using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Entities;

namespace WorkoutTracking.Data.Extension
{
    public static class SeedDataExtention
    {
        public static ModelBuilder Seed(this ModelBuilder modelBuilder)
        {
            SeedTrainingCategories(modelBuilder.Entity<TrainingCategory>());
            return modelBuilder;
        }

        public static void SeedTrainingCategories(EntityTypeBuilder<TrainingCategory> typeBuilder)
        {
            typeBuilder.HasData(
                new TrainingCategory() { Id = 1, Name = "Aerobic" },
                new TrainingCategory() { Id = 2, Name = "Anaerobic" },
                new TrainingCategory() { Id = 3, Name = "Strength" },
                new TrainingCategory() { Id = 4, Name = "Calisthenic" },
                new TrainingCategory() { Id = 5, Name = "Stretching" },
                new TrainingCategory() { Id = 6, Name = "Physical therapy" },
                new TrainingCategory() { Id = 7, Name = "Yoga" }
                );
        }
    }
}
