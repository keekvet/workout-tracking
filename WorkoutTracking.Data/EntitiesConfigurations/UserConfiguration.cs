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
            builder.HasIndex(u => u.Name).IsUnique();
            
            builder.Property(u => u.Name).HasMaxLength(50).IsRequired();
            builder.Property(u => u.Password).HasMaxLength(20).IsRequired();
            builder.Property(u => u.Salt).HasMaxLength(20).IsRequired();
            builder.Property(u => u.Access).IsRequired();

            builder.Ignore(u => u.JwtToken);

            builder
                .HasMany(u => u.PublicTrainingTemplates)
                .WithMany(t => t.Users);

            builder
                .HasMany(u => u.FriendsTo)
                .WithMany(u => u.FriendsFrom)
                .UsingEntity(x => x.ToTable("UserFriends"));

            builder
                .HasMany(u => u.Followers)
                .WithMany(u => u.Following)
                .UsingEntity(x => x.ToTable("UserFollowers"));
        }
    }
}
