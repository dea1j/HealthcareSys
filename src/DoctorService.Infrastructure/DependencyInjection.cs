using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DoctorService.Domain.Repositories;
using DoctorService.Infrastructure.Persistence;
using DoctorService.Infrastructure.Repositories;

namespace DoctorService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                });
        });

        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<IAvailabilitySlotRepository, AvailabilitySlotRepository>();

        return services;
    }
}