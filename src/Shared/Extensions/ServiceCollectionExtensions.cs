using API_Rest.Shared.Definitions;
using API_Rest.Shared.Services;
using FluentValidation;

namespace ApiTemplate.Shared.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddApplicationSevices(this IServiceCollection services)
    {
        services.RegisterModules(typeof(Program));
        services.AddSingleton<IMetricTracker, MetricTracker>();

        return services;
    }

    internal static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<Program>(ServiceLifetime.Scoped);
        services.AddScoped<IValidatorService, ValidatorService>();

        return services;
    }

    internal static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Program).Assembly);

        return services;
    }
}
