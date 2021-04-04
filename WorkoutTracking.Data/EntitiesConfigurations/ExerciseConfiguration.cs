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
    class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
            
            builder.Property(e => e.Position).IsRequired();
            
            builder.Property(e => e.Note).HasMaxLength(500);

            builder
                .HasOne(e => e.TrainingTemplate)
                .WithMany(t => t.Exercises)
                .HasForeignKey(e => e.TrainingTemplateId)
                .IsRequired();
        }
    }
}
