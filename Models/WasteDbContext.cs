using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CasusWebApps.Models
{
    public class WasteDbContext : DbContext
    {
        public WasteDbContext(DbContextOptions<WasteDbContext> options) :
            base(options)
        {
        }

        public DbSet<ImageHandler> ImageHandlers { get; set; }
//        public DbSet<ImageTag> ImageTags { get; set; }
        public DbSet<AnnotationModel> AnnotationModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ImageHandler>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<AnnotationModel>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<AnnotationModel>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<AnnotationModel>()
                .Property(a => a.BoundingBoxX)
                .HasColumnName("BoundingBoxX");

            modelBuilder.Entity<AnnotationModel>()
                .Property(a => a.BoundingBoxY)
                .HasColumnName("BoundingBoxY");

            modelBuilder.Entity<AnnotationModel>()
                .Property(a => a.BoundingBoxWidth)
                .HasColumnName("BoundingBoxWidth");

            modelBuilder.Entity<AnnotationModel>()
                .Property(a => a.BoundingBoxHeight)
                .HasColumnName("BoundingBoxHeight");

            base.OnModelCreating(modelBuilder);
        }
    }
}