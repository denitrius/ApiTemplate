using ApiTemplate.Modules.Customers.Core;
using ApiTemplate.Modules.Customers.Repositories;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Prometheus;
using V1 = ApiTemplate.Modules.Customers.Endpoints.V1;
using V2 = ApiTemplate.Modules.Customers.Endpoints.V2;

namespace ApiTemplate.Modules;

//[ApiController]
public class CustomersModule : IModule
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        //V1
        endpoints.MapGet("/v1/customers/{uuid:required}", V1.GetById.Handler.Handle)
            .Produces<Customer>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound).WithTags("V1");
        endpoints.MapPost("/v1/customers", V1.Create.Handler.Handle).WithTags("V1", "V2");

        ////V2
        endpoints.MapGet("/v2/customers/{uuid:required}", V2.GetById.Handler.Handle)
            .Produces<Customer>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound).WithTags("V2");

        //endpoints.MapPost("/v2/customers", V2.Create.Handler.Handle).WithTags("V2");
        return endpoints;
    }

    public IServiceCollection RegisterDependencies(IServiceCollection services)
    {
        services.AddSingleton<ICustomerRepository, CustomerRepository>();
        return services;
    }
}