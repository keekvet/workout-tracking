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
    public class FriendRequestConfiguration : IEntityTypeConfiguration<FriendRequest>
    {
        public void Configure(EntityTypeBuilder<FriendRequest> builder)
        {
            builder.HasKey(f => new { f.RequestFromId, f.RequestToId });

            builder.Property(f => f.IsRefused).HasDefaultValue(false).IsRequired();

            builder
                .HasOne(f => f.RequestTo)
                .WithMany(u => u.SendedFriendRequests)
                .HasForeignKey(f => f.RequestToId)
                .HasPrincipalKey(u => u.Id)
                .HasConstraintName("Receiver")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(f => f.RequestFrom)
                .WithMany(u => u.ReceivedFriendRequests)
                .HasForeignKey(f => f.RequestFromId)
                .HasPrincipalKey(u => u.Id)
                .HasConstraintName("Sender")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
