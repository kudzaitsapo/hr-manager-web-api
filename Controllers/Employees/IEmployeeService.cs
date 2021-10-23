using HrMan.Controllers.Employees.Dtos;
using HrMan.Models.Dtos;
using System;
using System.Threading.Tasks;

namespace HrMan.Controllers.Employees
{
    public interface IEmployeeService
    {
        Task<GenericResponseDto<EmployeeResponseDto>> CreateEmployeeAsync(EmployeeCreationRequestDto requestDto);

        Task<PagedResponse<EmployeeResponseDto>> GetAsync(int page, int limit);

        Task<GenericResponseDto<EmployeeResponseDto>> GetEmployeeAsync(Guid id);

        Task<GenericResponseDto<EmployeeResponseDto>> UpdateEmployeeAsync(Guid id);

        Task<GenericResponseDto<bool>> DeleteEmployeeAsync(Guid id);
    }
}
