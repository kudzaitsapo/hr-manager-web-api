using HrMan.Controllers.Departments.Dto;
using HrMan.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrMan.Controllers.Departments
{
    [Route("api/[controller]")]
    [Authorize()]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _service;

        public DepartmentsController(IDepartmentService service)
        {
            _service = service;
        }

        [HttpGet, Route("")]
        public async Task<ActionResult<PagedResponse<DepartmentResponseDto>>> GetDepartments(int? page, int? limit)
        {
            var fullPage = page ?? 1;
            var pageSize = limit ?? 10;
            var response = await _service.GetAsync(fullPage, pageSize);
            Response.StatusCode = response.Error != null ? response.Error.ErrorCode : StatusCodes.Status200OK;
            return new JsonResult(response);
        }

        [HttpGet, Route("{id}")]
        public async Task<ActionResult<GenericResponseDto<DepartmentResponseDto>>> GetSingleDepartment(Guid id)
        {
            var response = await _service.GetSingleAsync(id);
            Response.StatusCode = response.StatusCode ?? StatusCodes.Status200OK;
            return new JsonResult(response);
        }

        [HttpPost, Route("")]
        public async Task<ActionResult<GenericResponseDto<DepartmentResponseDto>>> CreateDepartment(DepartmentCreationRequestDto request)
        {
            var response = await _service.CreateAsync(request);
            Response.StatusCode = response.StatusCode ?? StatusCodes.Status201Created;
            return new JsonResult(response);
        }     

        [HttpPut, Route("{id}")]
        public async Task<ActionResult<GenericResponseDto<DepartmentResponseDto>>> UpdateDepartment(Guid id, DepartmentCreationRequestDto request)
        {
            var response = await _service.UpdateAsync(id, request);
            Response.StatusCode = response.StatusCode ?? StatusCodes.Status201Created;
            return new JsonResult(response);
        }

    }
}
