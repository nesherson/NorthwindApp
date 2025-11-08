using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindApp.Domain;

namespace NorthwindApp.Infrastructure;

public class ApplicationUserEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("AspNetUsers");
        
        builder.HasOne(up => up.UserProfile)
            .WithOne(u => u.User)
            .HasForeignKey<UserProfile>(up => up.UserId)
            .IsRequired();
    }
}