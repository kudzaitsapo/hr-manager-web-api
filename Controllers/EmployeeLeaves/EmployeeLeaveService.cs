using AutoMapper;
using HrMan.Controllers.EmployeeLeaves.Dto;
using HrMan.Models.Db;
using HrMan.Models.Dtos;
using HrMan.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace HrMan.Controllers.EmployeeLeaves
{
    public class EmployeeLeaveService : IEmployeeLeaveService
    {
        private readonly EmployeeDbContext _context;
        private readonly IMapper _mapper;

        public EmployeeLeaveService(EmployeeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenericResponseDto<LeaveResponseDto>> CreateAsync(LeaveRequestDto request)
        {
            var response = new GenericResponseDto<LeaveResponseDto>();

            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == request.EmployeeId);

            if (employee == null)
            {
                response.Error = new ErrorResponseDto()
                {
                    ErrorCode = 404,
                    Message = "Employee does not exist in the system!"
                };
                response.StatusCode = 404;

            } else
            {
                var leave = _mapper.Map<EmployeeLeave>(request);
                leave.Employee = employee;

                try
                {
                    _context.EmployeeLeaves.Add(leave);
                    await _context.SaveChangesAsync();
                    response.Result = _mapper.Map<LeaveResponseDto>(leave);
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
            }

            return response;
        }

        public async Task<PagedResponse<LeaveResponseDto>> GetAsync(int page, int limit)
        {
            var response = new PagedResponse<LeaveResponseDto>();
            try
            {
                if (page >= 1 && limit >= 1)
                {
                    var leaveQueryable = _context.EmployeeLeaves.AsQueryable();
                    var pagedLeaves = await leaveQueryable.Include(l => l.Employee)
                                                .ThenInclude(e => e.User)
                                                .ToPagedListAsync(page, limit);

                    response.Result = _mapper.Map<List<LeaveResponseDto>>(pagedLeaves.ToList());
                    response.TotalPages = pagedLeaves.PageCount;
                    response.Page = pagedLeaves.PageNumber;
                    response.PerPage = pagedLeaves.PageSize;


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

        public async Task<PagedResponse<LeaveResponseDto>> GetByEmployeeAsync(Guid employeeId, int page, int limit)
        {
            var response = new PagedResponse<LeaveResponseDto>();
            try
            {
                if (page >= 1 && limit >= 1)
                {
                    var leaveQueryable = _context.EmployeeLeaves.Where(l => l.Employee.Id == employeeId).AsQueryable();
                    var pagedLeaves = await leaveQueryable.ToPagedListAsync(page, limit);

                    response.Result = _mapper.Map<List<LeaveResponseDto>>(pagedLeaves.ToList());
                    response.TotalPages = pagedLeaves.PageCount;
                    response.Page = pagedLeaves.PageNumber;
                    response.PerPage = pagedLeaves.PageSize;


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

        public async Task<GenericResponseDto<LeaveResponseDto>> GetLeaveAsync(Guid id)
        {
            var response = new GenericResponseDto<LeaveResponseDto>();
            try
            {
                var leave = await _context.EmployeeLeaves.Include(l => l.Employee)
                                .ThenInclude(e => e.User)
                                .FirstOrDefaultAsync(l => l.Id == id);

                if (leave != null)
                {
                    response.Result = _mapper.Map<LeaveResponseDto>(leave);
                    response.StatusCode = 200;
                }
                else
                {
                    response.Error = new ErrorResponseDto()
                    {
                        ErrorCode = 404,
                        Message = "The employee leave was not found!"
                    };
                    response.StatusCode = 404;

                }
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

        public async Task<GenericResponseDto<LeaveResponseDto>> UpdateLeaveStatusAsync(Guid id, LeaveUpdateRequestDto requestDto)
        {
            var response = new GenericResponseDto<LeaveResponseDto>();

            var leave = await _context.EmployeeLeaves.FirstOrDefaultAsync(l => l.Id == id);

            if (leave != null)
            {
                leave.ApprovalStatus = (ApprovalStatus)requestDto.ApprovalStatus;
                if ((ApprovalStatus)requestDto.ApprovalStatus == ApprovalStatus.Approved && !requestDto.ApprovedBy.HasValue)
                {
                    response.Error = new ErrorResponseDto()
                    {
                        ErrorCode = 400,
                        Message = "Please specify the person who approved the leave!"
                    };
                    response.StatusCode = 400;
                } else
                {
                    if (requestDto.ApprovedBy.HasValue)
                    {
                        var approver = await _context.Employees.FirstOrDefaultAsync(e => e.Id == requestDto.ApprovedBy.Value);
                        leave.ApprovedBy = approver;
                    }
                }

                try
                {
                    await _context.SaveChangesAsync();
                    response.Result = _mapper.Map<LeaveResponseDto>(leave);
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
            } else
            {
                response.Error = new ErrorResponseDto()
                {
                    ErrorCode = 404,
                    Message = "The employee leave was not found!"
                };
                response.StatusCode = 404;
            }

            return response;
        }
    }
}
