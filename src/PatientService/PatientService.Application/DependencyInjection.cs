using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PatientService.Application.Mappings;

namespace PatientService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        
        services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(assembly));

        services.AddValidatorsFromAssembly(assembly);
        
        MappingConfig.RegisterMappings();
        return services;
    }
}