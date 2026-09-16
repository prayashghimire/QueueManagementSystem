using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QueueMS.Appilication.Interfaces;
using QueueMS.Appilication.Services;
using QueueMS.Domain.Models.UserModels;
using QueueMS.Infrastructure.DatabaseContext;

namespace QueueMS.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthService, AuthService>();

        services.AddDbContext<QueueMSDatabaseContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                .EnableSensitiveDataLogging());

        services.AddIdentity<User, IdentityRole<int>>()
            .AddEntityFrameworkStores<QueueMSDatabaseContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}
