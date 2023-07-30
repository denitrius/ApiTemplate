using AutoMapper;
using ApiTemplate.Modules.Customers.Core;
using ApiTemplate.Modules.Customers.Repositories;
using ApiTemplate.Shared.Definitions;
using Asp.Versioning;

namespace ApiTemplate.Modules.Customers.Endpoints.V2.Create;

public class Handler
{
    //[ApiVersion("2.0")]
    public static async Task<IResult> Handle(Request request, IValidatorService validator, IMapper mapper,
        ICustomerRepository repository, CancellationToken cancellation)
    {
        await validator.ValidateAsync(request, cancellation);

        await repository.AddAsync(mapper.Map<Customer>(request), cancellation);

        return Results.Created($"/customers/{request.Uuid}", null);
    }
}
