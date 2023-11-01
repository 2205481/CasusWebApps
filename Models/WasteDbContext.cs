using CasusWebApps.Data;
using CasusWebApps.Models;
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

        public DbSet<ImageHandler> ImageHandlers {  get; set; }

    }
}
