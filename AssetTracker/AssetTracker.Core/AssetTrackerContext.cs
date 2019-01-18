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
        //public DbSet<Location> Locations { get; set; }
        //public DbSet<Status> Statuses { get; set; }
        //public DbSet<Type> Types { get; set; }

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

            modelBuilder.Entity<Asset>()
                .HasMany(l => l.AssetLocations)
                .WithOne(a => a.Asset)
                .IsRequired();

            modelBuilder.Entity<Asset>()
                .HasOne(s => s.Status)
                .WithMany(m => m.Assets)
                .HasForeignKey(k => k.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Asset>()
                .HasOne(s => s.Type)
                .WithMany(m => m.Assets)
                .HasForeignKey(k => k.TypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AssetLocation>()
                .HasOne(o => o.Location)
                .WithMany(l => l.AssetLocations)
                .HasForeignKey(k => k.LocationId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Location>()
                .HasOne(l => l.Organization)
                .WithMany(m => m.Locations)
                .HasForeignKey(k => k.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Status>()
                .HasOne(l => l.Organization)
                .WithMany(a => a.Statuses)
                .HasForeignKey(k => k.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Type>()
                .HasOne(l => l.Organization)
                .WithMany(a => a.Types)
                .HasForeignKey(k => k.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
