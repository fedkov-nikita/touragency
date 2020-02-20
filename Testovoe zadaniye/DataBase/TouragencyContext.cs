using Testovoe_zadaniye.Models;
using Microsoft.EntityFrameworkCore;

namespace Testovoe_zadaniye.DataBase
{
    public class TouragencyContext : DbContext
    {
        public TouragencyContext(DbContextOptions<TouragencyContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TouristTour>()
                .HasKey(bc => new { bc.TouristId, bc.TourId });
            modelBuilder.Entity<Tourist>()
                .HasKey(x => x.Touristid);
            modelBuilder.Entity<TouristTour>()
                .HasOne(bc => bc.Tourist)
                .WithMany(b => b.TouristTours)
                .HasForeignKey(bc => bc.TouristId);
            modelBuilder.Entity<Tour>()
                .HasKey(x => x.TourId);
            modelBuilder.Entity<TouristTour>()
                .HasOne(bc => bc.Tour)
                .WithMany(c => c.TouristTours)
                .HasForeignKey(bc => bc.TourId);
            modelBuilder.Entity<Guide>()
                .HasKey(x => x.GuideId);
            modelBuilder.Entity<Guide>()
                .HasMany(c => c.Tourists)
                .WithOne(e => e.Guide);

        }

        public DbSet<Tourist> Tourists { get; set; }
        public DbSet<Guide> Guides { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<TouristTour> TouristTour { get; set; }


    }
}