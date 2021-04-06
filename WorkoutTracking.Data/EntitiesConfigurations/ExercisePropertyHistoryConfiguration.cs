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
    public class ExercisePropertyHistoryConfiguration : IEntityTypeConfiguration<ExercisePropertyHistory>
    {
        

        public void Configure(EntityTypeBuilder<ExercisePropertyHistory> builder)
        {
            builder.Property(e => e.Duration).IsRequired();

            builder
                .HasOne(e => e.ExerciseHistory)
                    .WithMany(e => e.Properties)
                    .HasForeignKey(e => e.ExerciseHistoryId)
                    .IsRequired();
        }
    }
}
