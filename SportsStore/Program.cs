using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

namespace SportsStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<StoreDbContext>(opts => {
                opts.UseSqlServer(
                builder.Configuration["ConnectionStrings:SportsStoreConnection"]);
            });

            builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute("pagination",
                "Products/Page{productPage}",
                new { Controller = "Product", action = "Index" }
                );

            app.MapControllerRoute("catpage",
                "Products/{category}/Page{productPage:int}",
                new { Controller = "Product", action = "Index"}
                );

            app.MapDefaultControllerRoute();

            SeedData.EnsurePopulated(app);

            app.Run();
        }
    }
}
