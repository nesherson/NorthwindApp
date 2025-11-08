using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindApp.Domain;

namespace NorthwindApp.Infrastructure;

public class UserProfileEntityTypeConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(30);
        
        builder.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(30);
        
        builder.Property(e => e.ProfilePictureUrl)
            .HasMaxLength(255);
    }
}