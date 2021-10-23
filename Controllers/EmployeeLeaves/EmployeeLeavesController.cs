using HrMan.Controllers.EmployeeLeaves.Dto;
using HrMan.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrMan.Controllers.EmployeeLeaves
{
    [Route("api/[controller]")]
    [Authorize()]
    [ApiController]
    public class EmployeeLeavesController : ControllerBase
    {
        private readonly IEmployeeLeaveService _service;

        public EmployeeLeavesController(IEmployeeLeaveService service)
        {
            _service = service;
        }

        [HttpPost, Route("")]
        public async Task<ActionResult<GenericResponseDto<LeaveResponseDto>>> CreateLeaveAsync(LeaveRequestDto request)
        {
            var response = await _service.CreateAsync(request);
            Response.StatusCode = response.StatusCode ?? StatusCodes.Status201Created;
            return new JsonResult(response);
        }

        [HttpGet, Route("")]
        public async Task<ActionResult<PagedResponse<LeaveResponseDto>>> GetLeaves(int? page, int? limit)
        {
            var fullPage = page ?? 1;
            var pageSize = limit ?? 10;

            var response = await _service.GetAsync(fullPage, pageSize);
            Response.StatusCode = response.Error != null ? response.Error.ErrorCode : StatusCodes.Status200OK;
            return new JsonResult(response);
        }

        [HttpGet, Route("{id}")]
        public async Task<ActionResult<GenericResponseDto<LeaveResponseDto>>> GetLeave(Guid id)
        {
            var response = await _service.GetLeaveAsync(id);
            Response.StatusCode = response.StatusCode ?? StatusCodes.Status200OK;
            return new JsonResult(response);
        }

        [HttpGet, Route("Employees/{employeeId}")]
        public async Task<ActionResult<PagedResponse<LeaveResponseDto>>> GetEmployeeLeaves(Guid employeeId, int? page, int? limit)
        {
            var fullPage = page ?? 1;
            var pageSize = limit ?? 10;

            var response = await _service.GetByEmployeeAsync(employeeId, fullPage, pageSize);
            Response.StatusCode = response.Error != null ? response.Error.ErrorCode : StatusCodes.Status200OK;
            return new JsonResult(response);
        }

        [HttpPut, Route("{id}")]
        public async Task<ActionResult<GenericResponseDto<LeaveResponseDto>>> UpdateLeaveStatus(Guid id, LeaveUpdateRequestDto requestDto)
        {
            var response = await _service.UpdateLeaveStatusAsync(id, requestDto);
            Response.StatusCode = response.StatusCode ?? StatusCodes.Status200OK;
            return new JsonResult(response);
        }

       
    }
}
