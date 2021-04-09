using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Entities;

namespace WorkoutTracking.Data.EntitiesConfigurations
{
    public class ActiveTrainingConfiguration : IEntityTypeConfiguration<ActiveTraining>
    {
        public void Configure(EntityTypeBuilder<ActiveTraining> builder)
        {

            builder
                .Property(a => a.ExerciseDonePosition)
                .IsRequired();

            builder
                .HasOne(a => a.User)
                .WithOne(u => u.ActiveTraining)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(a => a.TrainingTemplate)
                .WithOne(t => t.ActiveTraining)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(a => a.TrainingHistory)
                .WithOne(t => t.ActiveTraining)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
