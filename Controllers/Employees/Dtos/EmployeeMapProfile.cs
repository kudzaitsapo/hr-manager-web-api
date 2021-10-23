using AutoMapper;
using HrMan.Models.Entities;

namespace HrMan.Controllers.Employees.Dtos
{
    public class EmployeeMapProfile : Profile
    {
        public EmployeeMapProfile()
        {
            CreateMap<AddressRequestDto, Address>();

            CreateMap<Address, AddressResponseDto>();

            CreateMap<EmployeeCreationRequestDto, Employee>()
                .ForMember(e => e.HomeAddress, options => options.Ignore())
                .ForMember(e => e.Job, options => options.Ignore())
                .ForMember(e => e.User, options => options.Ignore());

            CreateMap<Employee, EmployeeResponseDto>()
                .ForMember(e => e.Firstname, options => options.MapFrom(e => e.User != null ? e.User.Firstname : null))
                .ForMember(e => e.Lastname, options => options.MapFrom(e => e.User != null ? e.User.Lastname : null))
                .ForMember(e => e.Middlename, options => options.MapFrom(e => e.User != null ? e.User.Middlename : null))
                .ForMember(e => e.Email, options => options.MapFrom(e => e.User != null ? e.User.Email : null))
                .ForMember(e => e.MobileNumber, options => options.MapFrom(e => e.User != null ? e.User.MobileNumber : null));


        }
    }
}
