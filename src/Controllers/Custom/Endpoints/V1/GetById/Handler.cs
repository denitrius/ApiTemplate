using AutoMapper;
using ApiTemplate.Modules.Customers.Repositories;
using Asp.Versioning;

namespace ApiTemplate.Modules.Customers.Endpoints.V1.GetById;

public class Handler
{
    [ApiVersion("1.0")]
    public static async Task<IResult> Handle(string uuid, ICustomerRepository repository, IMapper mapper,
        CancellationToken cancellation)
    {
        var customer = await repository.GetByIdAsync(uuid, cancellation);

        return Results.Ok(mapper.Map<Response>(customer));
    }
}
