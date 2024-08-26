using DemoMiddleWare.Middlewares;
using Microsoft.Extensions.FileProviders;

namespace DemoMiddleWare
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

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

            // Ajouter le middleware personnalisé FranceIPMiddleware
            app.UseMiddleware<FranceIPMiddleware>();


            // Ajouter ca pour montrer qu'on peut le faire inline et on peut ajouter des infos qu'on peut récupérer dans la suite ou dans Index()
            app.Use(async (context, next) =>
            {
                // Ajoute une information dans context.Items
                context.Items["CustomInfo"] = "Informations personnalisées";
                await next.Invoke();
            });

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
