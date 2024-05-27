using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using StaffRegistry.EntityModels;

namespace StaffRegistry.Sqlite3;

public class RestaurantDB : DbContext
{
    public DbSet<Staff>? Staff { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseSqlite(ConstructConnectionString());
        builder
            .LogTo(Console.WriteLine, new[] { RelationalEventId.CommandExecuting })
        #if DEBUG
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
        #endif
        ;
        builder.UseLazyLoadingProxies();
    }

    private string ConstructConnectionString()
    {
        string dbFile = "restaurant.db";
        string resourceDir = "resources";
        string path = Path.Combine(
            Environment.CurrentDirectory,
            resourceDir,
            dbFile);
        return $"Data Source={path}";
    }

    // Using Fluent API for finer access control together with attributes.
    // https://learn.microsoft.com/en-us/ef/ef6/modeling/code-first/fluent/types-and-properties
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Staff>()
          .Property(staff => staff.Position)
          .IsRequired()
          .HasMaxLength(30);
    }
}
