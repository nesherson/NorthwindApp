using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindApp.Domain;

namespace NorthwindApp.Infrastructure;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(r => r.Abrv)
            .HasMaxLength(DataSchemaConstants.DEFAULT_ABBREVIATION_LENGTH)
            .IsRequired();
        builder.Property(r => r.Name)
            .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
            .IsRequired();

        builder.HasMany(r => r.Users)
            .WithOne(u => u.Role)
            .HasPrincipalKey(r => r.Id);
    }
}
