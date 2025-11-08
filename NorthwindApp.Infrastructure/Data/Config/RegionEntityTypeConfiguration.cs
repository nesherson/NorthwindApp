using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindApp.Domain;

namespace NorthwindApp.Infrastructure;

public class RegionEntityTypeConfiguration : IEntityTypeConfiguration<Region>
{
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(255);
        
        builder.HasMany(r => r.Territories)
            .WithOne(t => t.Region)
            .HasForeignKey(t => t.RegionId);
    }
}