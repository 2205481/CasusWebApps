using CasusWebApps.Data;
using CasusWebApps.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp.Web.DependencyInjection;

namespace CasusWebApps
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            
            builder.Services.AddDbContext<WasteDbContext>(
                DbContextOptions =>
                DbContextOptions.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(
                options => 
                options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<WasteDbContext>();
            
            builder.Services.AddControllersWithViews();

            builder.Services.AddImageSharp();

            var app = builder.Build();

            /*
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
      v9          app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            */
            app.UseImageSharp();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "processimage",
                pattern: "{controller=ImageProcessing}/{action=ProcessImage}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "addimage_create",
                pattern: "{controller=ImageUpload}/{action=Create}/{id?}");

            app.MapRazorPages();

            app.Run();
        }

    }
    
}