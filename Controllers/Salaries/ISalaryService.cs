using HrMan.Controllers.Salaries.Dto;
using HrMan.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrMan.Controllers.Salaries
{
    public interface ISalaryService
    {
        Task<GenericResponseDto<SalariesResponseDto>> CreateAsync(SalariesCreationRequestDto request);

        Task<PagedResponse<SalariesResponseDto>> GetAsync(int page, int limit);

        Task<GenericResponseDto<SalariesResponseDto>> GetSingleAsync(Guid id);

        Task<GenericResponseDto<SalariesResponseDto>> UpdateAsync(Guid id, SalariesCreationRequestDto request);
    }
}
