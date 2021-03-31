using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Data.Enums;

namespace WorkoutTracking.Data.EntitiesConfigurations
{
    public class FriendRequestConfiguration : IEntityTypeConfiguration<FriendRequest>
    {
        public void Configure(EntityTypeBuilder<FriendRequest> builder)
        {
            builder.HasKey(f => new { f.RequestFromId, f.RequestToId });

            builder.Property(f => f.State).HasDefaultValue(FriendRequestState.Undefined).IsRequired();

            builder
                .HasOne(f => f.RequestFrom)
                .WithMany(u => u.SendedFriendRequests)
                .HasForeignKey(f => f.RequestFromId)
                .HasPrincipalKey(u => u.Id)
                .HasConstraintName("Sender")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction); 
            
            builder
                .HasOne(f => f.RequestTo)
                .WithMany(u => u.ReceivedFriendRequests)
                .HasForeignKey(f => f.RequestToId)
                .HasPrincipalKey(u => u.Id)
                .HasConstraintName("Receiver")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
