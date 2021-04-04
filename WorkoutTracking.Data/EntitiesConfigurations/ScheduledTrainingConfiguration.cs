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
    public class ScheduledTrainingConfiguration : IEntityTypeConfiguration<ScheduledTraining>
    {
        public void Configure(EntityTypeBuilder<ScheduledTraining> builder)
        {
            builder.Property(s => s.Note).HasMaxLength(500);
            builder.Property(s => s.Day).IsRequired();
            builder.Property(s => s.StartTime).IsRequired();
            builder
                .HasOne(s => s.Template)
                .WithMany(t => t.ScheduledTrainings)
                .HasForeignKey(s => s.TrainingTemplateId)
                .IsRequired();
        }
    }
}
