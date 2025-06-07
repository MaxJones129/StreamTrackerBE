using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using StreamTracker.Data;

namespace StreamTracker.Models
{
    public class StreamTrackerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<VideoUrl> VideoUrls { get; set; }


        public StreamTrackerDbContext(DbContextOptions<StreamTrackerDbContext> options) : base(options)
        {
        }

        // This method will initialize the database asynchronously and populate it with sample data if required.
        public async Task InitializeDatabaseAsync()
        {
            // Ensure the database is created if it does not exist
            await Database.EnsureCreatedAsync();

          
        }

    // Optional: Override OnModelCreating for more advanced configuration (like cascading deletes, etc.)
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      SeedData.SeedAll(modelBuilder);
    }
    }
}
