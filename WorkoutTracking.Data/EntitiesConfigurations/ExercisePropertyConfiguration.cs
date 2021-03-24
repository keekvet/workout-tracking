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
    public class ExercisePropertyConfiguration : IEntityTypeConfiguration<ExerciseProperty>
    {
        public void Configure(EntityTypeBuilder<ExerciseProperty> builder)
        {
            builder.Property(e => e.IsContinious).IsRequired();
            builder.Property(e => e.Duration).IsRequired();
            builder
                .HasOne(e => e.Exercise)
                .WithMany(e => e.Properties)
                .IsRequired();
        }
    }
}
