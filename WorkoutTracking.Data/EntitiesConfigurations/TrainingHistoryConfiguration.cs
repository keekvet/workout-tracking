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
    public class TrainingHistoryConfiguration : IEntityTypeConfiguration<TrainingHistory>
    {
        public void Configure(EntityTypeBuilder<TrainingHistory> builder)
        {
            builder.Property(t => t.Name).HasMaxLength(50).IsRequired();

            builder.Property(t => t.Start).IsRequired();

            builder
                .HasOne(t => t.User)
                .WithMany(u => u.TrainingHistory)
                .HasForeignKey(t => t.UserId)
                .IsRequired();
        }
    }
}
