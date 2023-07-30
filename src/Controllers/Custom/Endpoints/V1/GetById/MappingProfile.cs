using AutoMapper;
using ApiTemplate.Modules.Customers.Core;

namespace ApiTemplate.Modules.Customers.Endpoints.V1.GetById;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Customer, Response>()
            .ForPath(dest => dest.Uuid, opts => opts.MapFrom(src => src.Uuid));
    }
}
