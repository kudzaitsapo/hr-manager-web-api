using AutoMapper;
using HrMan.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrMan.Controllers.Jobs.Dto
{
    public class JobMapProfile : Profile
    {
        public JobMapProfile()
        {
            CreateMap<JobCreationRequestDto, Job>()
                .ForMember(j => j.Department, options => options.Ignore())
                .ForMember(j => j.SalaryRange, options => options.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Job, JobResponseDto>();

        }
    }
}
