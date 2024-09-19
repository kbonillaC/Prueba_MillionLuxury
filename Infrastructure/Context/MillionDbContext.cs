using Luxury.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Property = Luxury.Domain.Models.Property;

namespace Infrastructure.Context
{
    public class MillionDbContext(DbContextOptions<MillionDbContext> options) : DbContext(options)
    {
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Property> Property { get; set; }
        public DbSet<PropertyImage> PropertyImage { get; set; }
        public DbSet<PropertyTrace> PropertyTrace { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>().ToTable("Owner");
            modelBuilder.Entity<Property>().ToTable("Property");
            modelBuilder.Entity<PropertyImage>().ToTable("PropertyImage");
            modelBuilder.Entity<PropertyTrace>().ToTable("PropertyTrace");
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Owner>()
                .HasKey(o => o.IdOwner);

            modelBuilder.Entity<Property>(entity =>
            {
                entity.HasKey(p => p.IdProperty);
            });

            modelBuilder.Entity<PropertyImage>(entity =>
            {
                entity.HasKey(pi => pi.IdPropertyImage);
            });

            modelBuilder.Entity<PropertyTrace>(entity =>
            {
                entity.HasKey(pt => pt.IdPropertyTrace);
            });
        }
    }
}
