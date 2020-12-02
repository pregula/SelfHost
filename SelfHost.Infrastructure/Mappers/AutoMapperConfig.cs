using AutoMapper;
using SelfHost.Domain;
using SelfHost.Infrastructure.DTO;

namespace SelfHost.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Company, CompanyDto>();
                cfg.CreateMap<Employee, EmployeeDto>()
                .ForMember(dto => dto.JobTitle, m => m.MapFrom(e => e.JobTitle.ToString()));
                cfg.CreateMap<Company, CompanyDetailsDto>();
            })
            .CreateMapper();
    }
}