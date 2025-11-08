using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindApp.Domain;

namespace NorthwindApp.Infrastructure;

public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(50);
            
        builder.Property(e => e.Title)
            .HasMaxLength(50);

        builder.Property(e => e.TitleOfCourtesy)
            .HasMaxLength(25);
        
        builder.Property(e => e.Address)
            .HasMaxLength(100);

        builder.Property(e => e.City)
            .HasMaxLength(50);

        builder.Property(e => e.Region)
            .HasMaxLength(50);

        builder.Property(e => e.PostalCode)
            .HasMaxLength(10);
            
        builder.Property(e => e.Country)
            .HasMaxLength(50);

        builder.Property(e => e.HomePhone)
            .HasMaxLength(25);

        builder.Property(e => e.Extension)
            .HasMaxLength(4);
        
        builder.Property(e => e.PhotoPath)
            .HasMaxLength(255);
        
        builder.HasOne(e => e.ReportsTo)        
            .WithMany(e => e.Subordinates)      
            .HasForeignKey(e => e.ReportsToId)    
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}