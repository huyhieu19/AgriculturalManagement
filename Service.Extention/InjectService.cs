using Database;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Service.Contracts;

namespace Service.Extention
{
    public static class InjectService
    {
        public static void ConfigureLoggerService(this IServiceCollection service) => service.AddSingleton<ILoggerManager, LoggerManager>();
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<UserEntity, IdentityRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<FactDbContext>()
            .AddDefaultTokenProviders();
        }

    }
}