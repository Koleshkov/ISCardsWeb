using ISCardsWeb.Application.Common.Mappings;
using ISCardsWeb.Application;
using ISCardsWeb.Shared.Configurations;
using System.Reflection;
using ISCardsWeb.Persistance;
using ISCardsWeb.Server.Middlewares;
using ISCardsWeb.Shared.Responses;
using ISCardsWeb.Aplication.Services;

namespace ISCardsWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var services = builder.Services;
            var configuration = builder.Configuration;

            services.Configure<AuthenticationConfiguration>(
                configuration.GetSection("Authentication"));

            //Custom services
            services.AddAutoMapper(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AssemblyMappingProfile(typeof(IAppDbContext).Assembly));
            });

            services.AddPersistance(builder.Configuration);

            services.AddApplication(builder.Configuration);

   
            //Buid app
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCustomExceptionHandler();

            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapRazorPages();

            app.MapControllers();

            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}