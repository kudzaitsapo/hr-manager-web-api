using AutoMapper;
using HrMan.Controllers.Employees.Dtos;
using HrMan.Models.Db;
using HrMan.Models.Dtos;
using HrMan.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace HrMan.Controllers.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public EmployeeService(EmployeeDbContext context, IMapper mapper, UserManager<AppUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<GenericResponseDto<EmployeeResponseDto>> CreateEmployeeAsync(EmployeeCreationRequestDto requestDto)
        {
            var existingEmployee = await _context.Employees.FirstOrDefaultAsync(e => e.User.Email == requestDto.Email);
            var job = await _context.Jobs.FirstOrDefaultAsync(j => j.Id == requestDto.JobId);

            var response = new GenericResponseDto<EmployeeResponseDto>();

            if (existingEmployee != null || job == null)
            {
                response.Error = new ErrorResponseDto()
                {
                    ErrorCode = 400,
                    Message = "Either the employee's email is already registered or the job you have assigned to them is not registered in the system!"
                };
                response.StatusCode = 400;
            } else
            {
                var employeeUser = new AppUser()
                {
                    Firstname = requestDto.Firstname,
                    Lastname = requestDto.Lastname,
                    Middlename = requestDto.Middlename,
                    MobileNumber = requestDto.MobileNumber,
                    Email = requestDto.Email,
                    UserName = requestDto.Email
                };

                var result = await _userManager.CreateAsync(employeeUser, "@SecretPassword123");

                if (result.Succeeded)
                {
                    var address = _mapper.Map<Address>(requestDto.HomeAddress);
                    var employee = _mapper.Map<Employee>(requestDto);
                    employee.HomeAddress = address;
                    employee.Job = job;
                    employee.User = employeeUser;

                    try
                    {
                        _context.Addresses.Add(address);
                        _context.Employees.Add(employee);
                        await _context.SaveChangesAsync();

                        response.Result = _mapper.Map<EmployeeResponseDto>(employee);
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
                } else
                {
                    var error = "";
                    foreach (var identityError in result.Errors)
                    {
                        error += identityError.Description;
                    }

                    response.Error = new ErrorResponseDto { ErrorCode = 500, Message = "Failed to create user because of the following errors: " + error };
                }
            }

            return response;
        }

        public Task<GenericResponseDto<bool>> DeleteEmployeeAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResponse<EmployeeResponseDto>> GetAsync(int page, int limit)
        {
            var response = new PagedResponse<EmployeeResponseDto>();

            try
            {
                if (page >= 1 && limit >= 1)
                {

                    var employeeQueryable = _context.Employees.Include(e => e.User).Include(e => e.HomeAddress).Include(e => e.Job).AsQueryable();
                    var pagedEmployees = await employeeQueryable.ToPagedListAsync(page, limit);

                    response.Result = _mapper.Map<List<EmployeeResponseDto>>(pagedEmployees.ToList());
                    response.TotalPages = pagedEmployees.PageCount;
                    response.Page = pagedEmployees.PageNumber;
                    response.PerPage = pagedEmployees.PageSize;

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

        public async Task<GenericResponseDto<EmployeeResponseDto>> GetEmployeeAsync(Guid id)
        {
            var response = new GenericResponseDto<EmployeeResponseDto>();

            var employee = await _context.Employees.Include(e => e.HomeAddress)
                                                .Include(e => e.Job).ThenInclude(j => j.SalaryRange)
                                                .Include(e => e.Job).ThenInclude(j => j.Department)
                                                .ThenInclude(j => j.Organization)
                                                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee != null)
            {
                response.Result = _mapper.Map<EmployeeResponseDto>(employee);
                response.StatusCode = 200;
            } else
            {
                response.Error = new ErrorResponseDto()
                {
                    ErrorCode = 404,
                    Message = "Employee not found!"
                };
                response.StatusCode = 404;

            }

            return response;
        }

        public Task<GenericResponseDto<EmployeeResponseDto>> UpdateEmployeeAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
