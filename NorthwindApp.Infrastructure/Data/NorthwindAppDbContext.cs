using Microsoft.EntityFrameworkCore;
using NorthwindApp.Domain;
using System.Reflection;

namespace NorthwindApp.Infrastructure;

public class NorthwindAppDbContext : DbContext
{
    public NorthwindAppDbContext(DbContextOptions<NorthwindAppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}