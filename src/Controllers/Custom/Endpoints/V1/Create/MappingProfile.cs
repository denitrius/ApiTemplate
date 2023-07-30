using AutoMapper;
using ApiTemplate.Modules.Customers.Core;

namespace ApiTemplate.Modules.Customers.Endpoints.V1.Create;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Request, Customer>()
            .ForPath(dest => dest.Uuid, opts => opts.MapFrom(src => src.Uuid))
            .ForPath(dest => dest.State, opts => opts.Ignore());
    }
}
