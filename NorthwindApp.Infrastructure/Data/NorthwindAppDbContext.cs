using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NorthwindApp.Domain;

namespace NorthwindApp.Infrastructure;

public class NorthwindAppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
{
    public NorthwindAppDbContext(DbContextOptions<NorthwindAppDbContext> options) : base(options)
    {
        SavingChanges += OnSavingChanges;
    }
    
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeTerritory> EmployeeTerritories { get; set; }
    public DbSet<Territory> Territories { get; set; }
    public DbSet<Region> Regions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers");
        // modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    private void OnSavingChanges(object? sender, SavingChangesEventArgs e)
    {
        var entries = ChangeTracker.Entries();

        foreach (var entry in entries)
        {
            if (entry.Entity is not IEntityDateInfo entityWithDateInfo)
                continue;

            var now = DateTime.UtcNow;

            if (entry.State == EntityState.Modified)
            {
                entityWithDateInfo.DateModified = now;
            }
            else if (entry.State == EntityState.Added)
            {
                entityWithDateInfo.DateCreated = now;
            }
        }
    }
}