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
    public class ExerciseHistoryConfiguration : IEntityTypeConfiguration<ExerciseHistory>
    {
        public void Configure(EntityTypeBuilder<ExerciseHistory> builder)
        {
            builder.Property(e => e.Note).HasMaxLength(500);
            builder.Property(e => e.Start).IsRequired();

            builder
                .HasOne(e => e.Exercise)
                .WithMany(e => e.History);

            builder
                .HasOne(e => e.TrainingHistory)
                .WithMany(t => t.ExerciseHistory);
        }
    }
}
