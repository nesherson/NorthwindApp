using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindApp.Domain;

namespace NorthwindApp.Infrastructure;

public class TerritoryEntityTypeConfiguration : IEntityTypeConfiguration<Territory>
{
    public void Configure(EntityTypeBuilder<Territory> builder)
    {
        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(255);
    }
}