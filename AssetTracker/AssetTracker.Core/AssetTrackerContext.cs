using AssetTracker.Core.Entities;
using Common;
using Microsoft.EntityFrameworkCore;

namespace AssetTracker.Core
{
    public class AssetTrackerContext : DbContext, IDbContext
    {
        public AssetTrackerContext()
        { }

        public AssetTrackerContext(DbContextOptions<AssetTrackerContext> options) : base (options)
        {

        }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Type> Types { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDb; Database = AssetTrackerDb; Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrganizationUser>()
                .HasKey(k => new { k.OrganizationId, k.UserId });

            modelBuilder.Entity<AssetOrganization>()
                .HasKey(k => new { k.AssetId, k.OrganizationId });

            modelBuilder.Entity<AssetLocation>()
                .HasKey(k => new { k.AssetId, k.LocationId });
        }
    }
}
