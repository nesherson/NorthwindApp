using Microsoft.EntityFrameworkCore;
using NorthwindApp.Domain;
using System.Reflection;

namespace NorthwindApp.Infrastructure;

public class NorthwindAppDbContext : DbContext
{
    public NorthwindAppDbContext(DbContextOptions<NorthwindAppDbContext> options) : base(options)
    {
        SavingChanges += OnSavingChanges;
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
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