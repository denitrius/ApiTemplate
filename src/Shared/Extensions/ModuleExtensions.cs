using ApiTemplate.Shared.Definitions;

namespace ApiTemplate.Shared.Extensions;

internal static class ModuleExtensions
{
    internal static IServiceCollection RegisterModules(this IServiceCollection services, params Type[] scanMarkers)
    {
        var modules = new List<IModule>();

        foreach (var marker in scanMarkers)
        {
            modules.AddRange(DiscoverModules(marker));
        }

        foreach (var module in modules)
        {
            module.RegisterDependencies(services);
        }

        services.AddSingleton(modules as IReadOnlyCollection<IModule>);

        return services;
    }

    internal static WebApplication UseModules(this WebApplication app)
    {
        var modules = app.Services.GetRequiredService<IReadOnlyCollection<IModule>>();

        foreach (var module in modules)
        {
            module.MapEndpoints(app);
        }

        return app;
    }

    internal static IEnumerable<IModule> DiscoverModules(Type marker)
    {
        return marker.Assembly.ExportedTypes
                .Where(x => typeof(IModule).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IModule>();
    }
}
