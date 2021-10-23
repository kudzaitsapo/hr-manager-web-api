using AutoMapper;
using HrMan.Controllers.Organizations.Dto;
using HrMan.Models.Entities;

namespace HrMan.Controllers.Departments.Dto
{
    public class DepartmentMapProfile : Profile
    {
        public DepartmentMapProfile()
        {
            CreateMap<DepartmentCreationRequestDto, Department>()
                .ForMember(d => d.Organization, options => options.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Department, DepartmentResponseDto>();

        }
    }
}
