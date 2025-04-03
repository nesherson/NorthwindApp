using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindApp.Domain;

namespace NorthwindApp.Infrastructure;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.FirstName)
            .HasMaxLength(DataSchemaConstants.DEFAULT_FIRST_NAME_LENGTH)
            .IsRequired();
        builder.Property(u => u.LastName)
            .HasMaxLength(DataSchemaConstants.DEFAULT_LAST_NAME_LENGTH)
            .IsRequired();
        builder.Property(u => u.Email)
            .IsRequired();
        builder.Property(u => u.DateOfBirth)
            .IsRequired();
        builder.Property(u => u.PasswordHash)
            .IsRequired();
        builder.Property(u => u.PasswordSalt)
            .IsRequired();
        builder.Property(u => u.DateCreated)
            .IsRequired();
        builder.Property(u => u.RoleId)
            .IsRequired();

        builder.HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasPrincipalKey(u => u.Id)
            .HasForeignKey(u => u.RoleId);
    }
}
