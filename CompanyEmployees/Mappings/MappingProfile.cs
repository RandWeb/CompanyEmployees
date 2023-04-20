using AutoMapper;
using DomainModel.Entities;
using Shared.DataTransferObjects;

namespace CompanyEmployees.Mappings;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Company, CompanyDto>()
            .ForMember(c => c.FullAddress,
            opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));
           // .ForCtorParam("FullAddress",opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));

        CreateMap<CompanyForCreationDto, Company>();
        CreateMap<CompanyForUpdateDto, Company>();
        CreateMap<Employee, EmployeeDto>();
        CreateMap<EmployeeForCreationDto, Employee>();
        CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();

    }
}