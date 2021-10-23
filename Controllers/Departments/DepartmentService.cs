using AutoMapper;
using HrMan.Controllers.Departments.Dto;
using HrMan.Controllers.Organizations.Dto;
using HrMan.Models.Db;
using HrMan.Models.Dtos;
using HrMan.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace HrMan.Controllers.Departments
{
    public class DepartmentService : IDepartmentService
    {
        private readonly EmployeeDbContext _context;
        private readonly IMapper _mapper;

        public DepartmentService(EmployeeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenericResponseDto<DepartmentResponseDto>> CreateAsync(DepartmentCreationRequestDto request)
        {
            var response = new GenericResponseDto<DepartmentResponseDto>();
            var organization = await Task.Run(() => _context.Organizations.FirstOrDefault(o => o.Id == request.OrganizationId));

            if (organization != null)
            {
                try
                {
                    var department = _mapper.Map<Department>(request);
                    department.Organization = organization;

                    _context.Departments.Add(department);
                    await _context.SaveChangesAsync();
                    response.Result = _mapper.Map<DepartmentResponseDto>(department);
                    response.StatusCode = 201;

                } catch (Exception ex)
                {
                    response.Error = new ErrorResponseDto()
                    {
                        ErrorCode = 500,
                        Message = ex.Message
                    };
                    response.StatusCode = 500;
                }
            } else
            {
                response.Error = new ErrorResponseDto()
                {
                    ErrorCode = 404,
                    Message = "Organization not found!"
                };
                response.StatusCode = 404;
            }
            return response;
        }

        public Task<GenericResponseDto<bool>> DeleteAsync(Guid id)
        {
            // TODO: Implement Soft delete
            throw new NotImplementedException();
        }

        public async Task<PagedResponse<DepartmentResponseDto>> GetAsync(int page, int limit)
        {
            var response = new PagedResponse<DepartmentResponseDto>();

            try
            {
                if (page >= 1 && limit >= 1)
                {
                    var deptQueryable = await Task.Run(() => _context.Departments.Include(d => d.Organization).AsQueryable());
                    var departments = deptQueryable.ToPagedList(page, limit);

                    response.TotalPages = departments.PageCount;
                    response.Page = departments.PageNumber;
                    response.PerPage = departments.PageSize;
                    response.Result = _mapper.Map<List<DepartmentResponseDto>>(departments.ToList());

                } else
                {
                    response.Error = new ErrorResponseDto()
                    {
                        ErrorCode = 400,
                        Message = "The page number and page size must be greater than 1!"
                    };
                }

            } catch (Exception ex)
            {
                response.Error = new ErrorResponseDto()
                {
                    ErrorCode = 500,
                    Message = ex.Message
                };
            }

            return response;
        }

        public async Task<GenericResponseDto<DepartmentResponseDto>> GetSingleAsync(Guid id)
        {
            var response = new GenericResponseDto<DepartmentResponseDto>();

            var department = await Task.Run(() => _context.Departments.Include(d => d.Organization).FirstOrDefault(d => d.Id == id));
           

            if (department != null)
            {
                var departmentDto = _mapper.Map<DepartmentResponseDto>(department);
                response.Result = departmentDto;
                response.StatusCode = 200;

            } else
            {
                response.StatusCode = 404;
                response.Error = new ErrorResponseDto()
                {
                    ErrorCode = 404,
                    Message = "Department not found!"
                };
            }

            return response;
        }

        public async Task<GenericResponseDto<DepartmentResponseDto>> UpdateAsync(Guid id, DepartmentCreationRequestDto request)
        {
            var response = new GenericResponseDto<DepartmentResponseDto>();

            var department = await Task.Run(() => _context.Departments.FirstOrDefault(d => d.Id == id));

            var organization = await Task.Run(() => _context.Organizations.FirstOrDefault(o => o.Id == request.OrganizationId));

            if (department != null && organization != null)
            {
                var updatedDepartment = _mapper.Map(request, department);
                updatedDepartment.Organization = organization;

                await  _context.SaveChangesAsync();

                response.Result = _mapper.Map<DepartmentResponseDto>(updatedDepartment);
                response.StatusCode = 200;
            }
            else
            {
                response.StatusCode = 404;
                response.Error = new ErrorResponseDto()
                {
                    ErrorCode = 404,
                    Message = "Department or Organization not found!"
                };
            }

            return response;

        }
    }
}
