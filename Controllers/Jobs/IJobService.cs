using HrMan.Controllers.Jobs.Dto;
using HrMan.Models.Dtos;
using System;
using System.Threading.Tasks;

namespace HrMan.Controllers.Jobs
{
    public interface IJobService
    {

        // Async Job methods for use inside the JobsController
        Task<GenericResponseDto<JobResponseDto>> CreateJobAsync(JobCreationRequestDto requestDto);

        Task<PagedResponse<JobResponseDto>> GetAsync(int page, int limit);


        Task<GenericResponseDto<JobResponseDto>> GetJobAsync(Guid id);

        Task<GenericResponseDto<JobResponseDto>> UpdateJobAsync(Guid id, JobCreationRequestDto requestDto);

        Task<bool> DeleteJobAsync(Guid id);

    }
}
