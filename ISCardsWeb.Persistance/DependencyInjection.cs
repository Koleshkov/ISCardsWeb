using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ISCardsWeb.Shared.Models;
using ISCardsWeb.Aplication.Services;

namespace ISCardsWeb.Persistance
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlLite");

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(connectionString));

            services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireDigit=true;
                opt.Password.RequireNonAlphanumeric=true;
                opt.Password.RequireUppercase=true;
                opt.Password.RequiredLength = 8;
                opt.Password.RequiredUniqueChars = 1;
            }).AddEntityFrameworkStores<AppDbContext>()
              .AddDefaultTokenProviders();

            services.AddScoped<IAppDbContext, AppDbContext>();

            return services;
        }
    }
}
