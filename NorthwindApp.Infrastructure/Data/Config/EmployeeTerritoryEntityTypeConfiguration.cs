using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindApp.Domain;

namespace NorthwindApp.Infrastructure;

public class EmployeeTerritoryEntityTypeConfiguration : IEntityTypeConfiguration<EmployeeTerritory>
{
    public void Configure(EntityTypeBuilder<EmployeeTerritory> builder)
    {
        builder.HasOne(et => et.Employee)
            .WithMany(e => e.EmployeeTerritories)
            .HasForeignKey(et => et.EmployeeId);
        
        builder.HasOne(et => et.Territory)
            .WithMany(t => t.EmployeeTerritories)
            .HasForeignKey(et => et.EmployeeId);
    }
}