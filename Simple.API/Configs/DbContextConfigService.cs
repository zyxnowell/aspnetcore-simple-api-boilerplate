using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Simple.Intrastructure.Context;
using Simple.Intrastructure.Entities;
using System;

namespace Simple.API.Configs
{
    /// <summary>
    /// Register database and configure identity
    /// </summary>

    public static class DbContextConfigService
    {
        public static IServiceCollection RegisterDatabaseContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["ConnectionStrings:DefaultConnection"];

            services.AddDbContext<AppDBContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddDbContext<AppDBContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<AppUser, IdentityRole>(SetupAction())
                .AddEntityFrameworkStores<AppDBContext>()
                .AddDefaultTokenProviders()
                .AddUserManager<UserManager<AppUser>>();

            return services;
        }
        private static Action<IdentityOptions> SetupAction()
        {

            return option => {

                option.Password.RequireDigit = false;
                option.Password.RequireLowercase = false;
                option.Password.RequireUppercase = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequiredLength = 6;

                option.User.RequireUniqueEmail = true;
            };
        }
    }
}
