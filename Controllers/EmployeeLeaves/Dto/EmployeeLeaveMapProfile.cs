using AutoMapper;
using HrMan.Models.Entities;
using System;

namespace HrMan.Controllers.EmployeeLeaves.Dto
{
    public class EmployeeLeaveMapProfile : Profile
    {
        public EmployeeLeaveMapProfile()
        {
            CreateMap<LeaveRequestDto, EmployeeLeave>()
                .ForMember(l => l.Employee, options => options.Ignore())
                .ForMember(l => l.ApprovalStatus, options => options.MapFrom(source => ApprovalStatus.Pending))
                .ForMember(l => l.ApprovedBy, options => options.Ignore());

            CreateMap<EmployeeLeave, LeaveResponseDto>()
                .ForMember(l => l.ApprovalStatus, options =>
                                options.MapFrom(source => Enum.GetName(typeof(ApprovalStatus), source.ApprovalStatus)))
                .ForMember(l => l.LeaveType, options =>
                                options.MapFrom(source => Enum.GetName(typeof(LeaveType), source.LeaveType)));
        }
    }
}
