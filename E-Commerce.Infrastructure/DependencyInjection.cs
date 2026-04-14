using E_Commerce.Application.Interfaces.Persistence;
using E_Commerce.Application.Interfaces.Security;
using E_Commerce.Application.Interfaces.SMTP;
using E_Commerce.Infrastructure.Data;
using E_Commerce.Infrastructure.Security;
using E_Commerce.Infrastructure.SMTP;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        services.AddScoped<IDataContext, DataContext>();

        var jwtSettings = config.GetSection("JwtSettings").Get<JwtSettings>()!;
        services.AddSingleton(jwtSettings);
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IEmailService, EmailService>();

        return services;
    }
}