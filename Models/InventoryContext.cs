using Microsoft.EntityFrameworkCore;

namespace ShopifyBackend.Models
{
    public class InventoryContext : DbContext
    {
        public InventoryContext() { }

        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options) 
        { 

        }

        public DbSet<Inventory> Inventories { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Relationship>   Relationships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inventory>().ToTable("Inventory");
            modelBuilder.Entity<Location>().ToTable("Location");
            modelBuilder.Entity<Relationship>()
                .HasKey(r => new { r.InventoryId, r.LocationId });
        }
    }
}
