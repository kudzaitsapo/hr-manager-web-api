using AutoMapper;
using HrMan.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrMan.Controllers.Salaries.Dto
{
    public class SalaryMapProfile : Profile
    {
        public SalaryMapProfile()
        {
            CreateMap<SalariesCreationRequestDto, Salary>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Salary, SalariesResponseDto>()
                .ForMember(s => s.PayGrade, options => options.MapFrom(src => Enum.GetName(typeof(Grade), src.PayGrade)));
        }
    }
}
