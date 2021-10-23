using HrMan.Controllers.EmployeeLeaves.Dto;
using HrMan.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrMan.Controllers.EmployeeLeaves
{
    public interface IEmployeeLeaveService
    {
        Task<GenericResponseDto<LeaveResponseDto>> CreateAsync(LeaveRequestDto request);

        Task<PagedResponse<LeaveResponseDto>> GetAsync(int page, int limit);

        Task<PagedResponse<LeaveResponseDto>> GetByEmployeeAsync(Guid employeeId, int page, int limit);

        Task<GenericResponseDto<LeaveResponseDto>> GetLeaveAsync(Guid id);

        Task<GenericResponseDto<LeaveResponseDto>> UpdateLeaveStatusAsync(Guid id, LeaveUpdateRequestDto requestDto);
    }
}
