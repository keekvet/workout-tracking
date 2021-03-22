using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Entities;

namespace WorkoutTracking.Data.Context
{
    public class WorkoutContext : DbContext
    {
        public DbSet<Exercise> exercises { get; set; }
        public DbSet<TrainingTemplate> trainingTemplates { get; set; }
        public DbSet<User> users { get; set; }
        public WorkoutContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
