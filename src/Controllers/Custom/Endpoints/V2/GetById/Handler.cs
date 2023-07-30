using AutoMapper;
using ApiTemplate.Modules.Customers.Repositories;

namespace ApiTemplate.Modules.Customers.Endpoints.V2.GetById;

public class Handler
{
    public static async Task<IResult> Handle(string uuid, ICustomerRepository repository, IMapper mapper,
        CancellationToken cancellation)
    {
        var customer = await repository.GetByIdAsync(uuid, cancellation);

        return Results.Ok(mapper.Map<Response>(customer));
    }
}
