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
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Name).HasMaxLength(50).IsRequired();
            builder.Property(u => u.Access).IsRequired();

            builder
                .HasMany(u => u.PublicTrainingTemplates)
                .WithMany(t => t.Users);

            builder
                .HasMany(u => u.Friends)
                .WithMany(u => u.Friends);

            builder
                .HasMany(u => u.Followers)
                .WithMany(u => u.Following);
        }
    }
}
