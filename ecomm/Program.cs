using ecomm.Application.Interfaces;
using ecomm.Infrastructure.Services;
using Microsoft.Extensions.Options;

namespace ecomm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IUserServices, UserRepo>();
            builder.Services.AddScoped<ICategoryServies, CategoryRepo>();
            builder.Services.AddScoped<IProductServices, ProductRepo>();
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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();

        }
    }
}
