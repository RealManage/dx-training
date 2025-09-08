using Microsoft.EntityFrameworkCore;
using RealManage.PropertyManager.Models;

namespace RealManage.PropertyManager.Data;

public class PropertyContext(DbContextOptions<PropertyContext> options) : DbContext(options)
{
    public DbSet<Property> Properties { get; set; } = null!;
    
    // TODO: Add these via TDD
    // public DbSet<Valuation> Valuations { get; set; }
    // public DbSet<MaintenanceRequest> MaintenanceRequests { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Property>()
            .HasIndex(p => p.Address);
        
        // Seed some test data
        modelBuilder.Entity<Property>().HasData(
            new Property 
            { 
                Id = 1, 
                Address = "123 Main St", 
                City = "Dallas", 
                State = "TX", 
                ZipCode = "75201" 
            },
            new Property 
            { 
                Id = 2, 
                Address = "456 Oak Ave", 
                City = "Austin", 
                State = "TX", 
                ZipCode = "78701" 
            },
            new Property 
            { 
                Id = 3, 
                Address = "789 Elm Dr", 
                City = "Houston", 
                State = "TX", 
                ZipCode = "77001" 
            }
        );
    }
}