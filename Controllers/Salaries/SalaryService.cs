using AutoMapper;
using HrMan.Controllers.Salaries.Dto;
using HrMan.Models.Db;
using HrMan.Models.Dtos;
using HrMan.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace HrMan.Controllers.Salaries
{
    public class SalaryService : ISalaryService
    {
        private readonly EmployeeDbContext _context;
        private readonly IMapper _mapper;

        public SalaryService(EmployeeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenericResponseDto<SalariesResponseDto>> CreateAsync(SalariesCreationRequestDto request)
        {
            var response = new GenericResponseDto<SalariesResponseDto>();
            var salary = _mapper.Map<Salary>(request);
            try
            {
                _context.Salaries.Add(salary);
                await _context.SaveChangesAsync();
                response.Result = _mapper.Map<SalariesResponseDto>(salary);
                response.StatusCode = 201;
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


            return response;
        }

        public async Task<PagedResponse<SalariesResponseDto>> GetAsync(int page, int limit)
        {
            var response = new PagedResponse<SalariesResponseDto>();
            try
            {
                if (page >= 1 && limit >= 1)
                {
                    var salaryQueryable = _context.Salaries.AsQueryable();
                    var pagedSalaries = await salaryQueryable.ToPagedListAsync(page, limit);

                    response.Result = _mapper.Map<List<SalariesResponseDto>>(pagedSalaries.ToList());
                    response.TotalPages = pagedSalaries.PageCount;
                    response.Page = pagedSalaries.PageNumber;
                    response.PerPage = pagedSalaries.PageSize;

                } else
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

        public async Task<GenericResponseDto<SalariesResponseDto>> GetSingleAsync(Guid id)
        {
            var response = new GenericResponseDto<SalariesResponseDto>();

            var salary = await _context.Salaries.FirstOrDefaultAsync(s => s.Id == id);

            if (salary != null)
            {
                response.Result = _mapper.Map<SalariesResponseDto>(salary);
                response.StatusCode = 200;
            } else
            {
                response.Error = new ErrorResponseDto()
                {
                    ErrorCode = 404,
                    Message = "Salary range not found!"
                };
                response.StatusCode = 404;
            }

            return response;
        }

        public async Task<GenericResponseDto<SalariesResponseDto>> UpdateAsync(Guid id, SalariesCreationRequestDto request)
        {
            var response = new GenericResponseDto<SalariesResponseDto>();

            var salary = await _context.Salaries.FirstOrDefaultAsync(s => s.Id == id);

            if (salary != null)
            {

                try
                {
                    var updatedSalary = _mapper.Map(request, salary);

                    await _context.SaveChangesAsync();
                    response.Result = _mapper.Map<SalariesResponseDto>(updatedSalary);
                    response.StatusCode = 200;
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

                response.Result = _mapper.Map<SalariesResponseDto>(salary);
                response.StatusCode = 200;
            }
            else
            {
                response.Error = new ErrorResponseDto()
                {
                    ErrorCode = 404,
                    Message = "Salary range not found!"
                };
                response.StatusCode = 404;
            }

            return response;
        }
    }
}
