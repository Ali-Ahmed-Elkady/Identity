using FluentValidation;
using Identity_10_.Models.DataBase;
using Identity_10_.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity_10_
{
    public static class Dependancies 
    {
        public static IServiceCollection AddServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDb(configuration);
            services.AddScoped();
            services.AddValidatorsFromAssemblyContaining<Program>(); // program or your assembly marker 
            services.AddIdentity();
            return services;
        }
        public static IServiceCollection AddDb(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));
            return services;
        }
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            });
            return services;
        }
        public static IServiceCollection AddScoped(this IServiceCollection services)
        {

            return services;
        }

    }
}
