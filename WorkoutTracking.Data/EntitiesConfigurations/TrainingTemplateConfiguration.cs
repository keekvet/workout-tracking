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
    class TrainingTemplateConfiguration : IEntityTypeConfiguration<TrainingTemplate>
    {
        public void Configure(EntityTypeBuilder<TrainingTemplate> builder)
        {
            builder.Property(t => t.Name).HasMaxLength(50).IsRequired();
            builder.Property(t => t.Description).HasMaxLength(500);

            builder
                .HasOne(t => t.Creator)
                .WithMany(u => u.TrainingTemplates);

            builder
                .HasOne(t => t.Category)
                .WithMany(c => c.TrainingTemplates)
                .IsRequired();
        }
    }
}
