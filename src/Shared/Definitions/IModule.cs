namespace ApiTemplate.Shared.Definitions;

public interface IModule
{
    IServiceCollection RegisterDependencies(IServiceCollection services);
    IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints);
}
