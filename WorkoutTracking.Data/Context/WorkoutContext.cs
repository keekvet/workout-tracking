using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Data.EntitiesConfigurations;

namespace WorkoutTracking.Data.Context
{
    public class WorkoutContext : DbContext
    {
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseHistory> ExercisesHistory { get; set; }
        public DbSet<ExerciseProperty> ExercisesProperty { get; set; }
        public DbSet<PublicTrainingTemplate> PublicTrainingTemplates { get; set; }
        public DbSet<ScheduledTraining> ScheduledTrainings { get; set; }
        public DbSet<TrainingCategory> TrainingCategories { get; set; }
        public DbSet<TrainingHistory> TrainingHistories { get; set; }
        public DbSet<TrainingTemplate> TrainingTemplates { get; set; }
        public DbSet<User> Users { get; set; }

        public WorkoutContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ExerciseConfiguration());
            modelBuilder.ApplyConfiguration(new ExerciseHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new ExercisePropertyConfiguration());
            modelBuilder.ApplyConfiguration(new PublicTrainingTemplateConfiguration());
            modelBuilder.ApplyConfiguration(new ScheduledTrainingConfiguration());
            modelBuilder.ApplyConfiguration(new TrainingCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new TrainingHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new TrainingTemplateConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
