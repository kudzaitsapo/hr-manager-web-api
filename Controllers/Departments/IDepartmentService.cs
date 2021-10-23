using HrMan.Controllers.Departments.Dto;
using HrMan.Models.Dtos;
using System;
using System.Threading.Tasks;

namespace HrMan.Controllers.Departments
{
    public interface IDepartmentService
    {
        Task<GenericResponseDto<DepartmentResponseDto>> CreateAsync(DepartmentCreationRequestDto request);

        Task<PagedResponse<DepartmentResponseDto>> GetAsync(int page, int limit);

        Task<GenericResponseDto<DepartmentResponseDto>> GetSingleAsync(Guid id);

        Task<GenericResponseDto<DepartmentResponseDto>> UpdateAsync(Guid id, DepartmentCreationRequestDto request);

        Task<GenericResponseDto<bool>> DeleteAsync(Guid id);
    }
}
