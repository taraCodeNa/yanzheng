using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Yanzheng.Data;
using Yanzheng.Identity;
// Reference your DbContext project

// Reference your User entity project

// using Microsoft.AspNetCore.Identity;            // For AddIdentity, IdentityUser, etc.
// using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;
// Extension methods usually live in this namespace

public static class IdentityServiceCollectionExtensions
{
    public static IServiceCollection AddYanzhengDependencies(this IServiceCollection services,
        IConfiguration                                                               configuration)
    {
        // 1. Register the ApplicationDbContext
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                // Optional: Configure SQL Server specific options if needed
                sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        10,
                        TimeSpan.FromSeconds(30),
                        null);
                })
        );


        // 2. Register ASP.NET Core Identity services
        // Use your custom User entity and IdentityRole<long> for roles, and long for the key type
        services.AddIdentity<User, IdentityRole<long>>(options =>
            {
                // Configure Identity options here (e.g., password requirements, lockout settings)
                options.Password.RequireDigit           = true;
                options.Password.RequiredLength         = 8;
                options.Password.RequireLowercase       = true;
                options.Password.RequireUppercase       = true;
                options.Password.RequireNonAlphanumeric = true;
                options.User.RequireUniqueEmail         = true;
                options.SignIn.RequireConfirmedAccount  = false; // Set to true for email confirmation
                options.Lockout.DefaultLockoutTimeSpan  = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
            })
            // Configure Identity to use Entity Framework Core stores
            .AddEntityFrameworkStores<ApplicationDbContext>() // Specify ApplicationDbContext and long as the key type
            // Add default token providers (for password reset, email confirmation, etc.)
            .AddDefaultTokenProviders();

        return services;
    }
}