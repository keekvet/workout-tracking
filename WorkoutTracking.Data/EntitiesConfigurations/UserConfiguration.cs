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
            
            builder.Property(u => u.Name).HasMaxLength(20).IsRequired();
            builder.Property(u => u.PasswordHash).HasMaxLength(128).IsRequired();
            builder.Property(u => u.Salt).HasMaxLength(20).IsRequired();
            builder.Property(u => u.Access).IsRequired();
            builder.Property(u => u.About).HasMaxLength(1000);

            builder.Ignore(u => u.Jwt);

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
