using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CasusWebApps.Models
{
    public class WasteDbContext : DbContext
    {
        public WasteDbContext(DbContextOptions<WasteDbContext> options) :
            base(options)
        {

        }
        public DbSet<ImageHandler> ImageHandlers { get; set; }
        public DbSet<ImageTag> ImageTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ImageHandler>()
                .HasMany(ih => ih.ImageTags)
                .WithOne(it => it.ImageHandler)
                .HasForeignKey(it => it.ImageHandlerId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
