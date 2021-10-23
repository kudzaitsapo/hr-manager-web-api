using AutoMapper;
using HrMan.Controllers.Jobs.Dto;
using HrMan.Models.Db;
using HrMan.Models.Dtos;
using HrMan.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace HrMan.Controllers.Jobs
{
    public class JobService : IJobService
    {
        private readonly IMapper _mapper;
        private readonly EmployeeDbContext _context;

        public JobService(IMapper mapper, EmployeeDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<GenericResponseDto<JobResponseDto>> CreateJobAsync(JobCreationRequestDto requestDto)
        {
            var response = new GenericResponseDto<JobResponseDto>();

            var department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == requestDto.DepartmentId);
            var salaryRange = await _context.Salaries.FirstOrDefaultAsync(s => s.Id == requestDto.SalaryRangeId);


            if (department != null && salaryRange != null)
            {
                try
                {
                    var job = _mapper.Map<Job>(requestDto);

                    job.Department = department;
                    job.SalaryRange = salaryRange;

                    _context.Jobs.Add(job);
                    await _context.SaveChangesAsync();

                    response.StatusCode = 201;
                    response.Result = _mapper.Map<JobResponseDto>(job);

                }
                catch (Exception ex)
                {
                    response.Error = new ErrorResponseDto()
                    {
                        ErrorCode = 500,
                        Message = ex.Message
                    };
                }
            } else
            {
                response.Error = new ErrorResponseDto()
                {
                    ErrorCode = 400,
                    Message = "Department or salary range not found!"
                };
                response.StatusCode = 400;
            }


            return response;
        }

        public Task<bool> DeleteJobAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResponse<JobResponseDto>> GetAsync(int page, int limit)
        {
            var response = new PagedResponse<JobResponseDto>();
            try
            {
                if (page >= 1 && limit >= 1)
                {
                    var jobQueryable = _context.Jobs.Include(j => j.Department).Include(j => j.SalaryRange).AsQueryable();
                    var pagedJobs = await jobQueryable.ToPagedListAsync(page, limit);

                    response.Result = _mapper.Map<List<JobResponseDto>>(pagedJobs.ToList());
                    response.TotalPages = pagedJobs.PageCount;
                    response.Page = pagedJobs.PageNumber;
                    response.PerPage = pagedJobs.PageSize;

                }
                else
                {
                    response.Error = new ErrorResponseDto()
                    {
                        ErrorCode = 400,
                        Message = "The page number and page size must be greater than 1!"
                    };
                }

            }
            catch (Exception ex)
            {
                response.Error = new ErrorResponseDto()
                {
                    ErrorCode = 500,
                    Message = ex.Message
                };
            }

            return response;
        }

        public async Task<GenericResponseDto<JobResponseDto>> GetJobAsync(Guid id)
        {
            var response = new GenericResponseDto<JobResponseDto>();

            var job = await Task.Run(() => _context.Jobs.Include(j => j.Department).Include(j => j.SalaryRange).FirstOrDefault(j => j.Id == id));

            if (job != null)
            {
                response.StatusCode = 200;
                response.Result = _mapper.Map<JobResponseDto>(job);
            } else
            {
                response.StatusCode = 404;
                response.Error = new ErrorResponseDto()
                {
                    ErrorCode = 404,
                    Message = "Job not found!"
                };
            }

            return response;
        }

        public async Task<GenericResponseDto<JobResponseDto>> UpdateJobAsync(Guid id, JobCreationRequestDto requestDto)
        {
            var response = new GenericResponseDto<JobResponseDto>();

            var job = await _context.Jobs.FirstOrDefaultAsync(j => j.Id == id);
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == requestDto.DepartmentId);
            var salaryRange = await _context.Salaries.FirstOrDefaultAsync(s => s.Id == requestDto.SalaryRangeId);

            if (department != null && salaryRange != null && job != null)
            {
                try
                {
                    var updatedJob = _mapper.Map(requestDto, job);

                    updatedJob.Department = department;
                    updatedJob.SalaryRange = salaryRange;

                    await _context.SaveChangesAsync();

                    response.StatusCode = 201;
                    response.Result = _mapper.Map<JobResponseDto>(updatedJob);

                }
                catch (Exception ex)
                {
                    response.Error = new ErrorResponseDto()
                    {
                        ErrorCode = 500,
                        Message = ex.Message
                    };
                    response.StatusCode = 500;
                }
            }
            else
            {
                response.Error = new ErrorResponseDto()
                {
                    ErrorCode = 400,
                    Message = "Job or department or salary range not found!"
                };
                response.StatusCode = 400;
            }

            return response;
        }
    }
}
