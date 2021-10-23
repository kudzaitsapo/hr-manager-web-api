using HrMan.Controllers.Salaries.Dto;
using HrMan.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrMan.Controllers.Salaries
{
    [Route("api/[controller]")]
    [Authorize()]
    [ApiController]
    public class SalariesController : ControllerBase
    {
        private readonly ISalaryService _service;

        public SalariesController(ISalaryService service)
        {
            _service = service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("")]
        public async Task<ActionResult<GenericResponseDto<SalariesResponseDto>>> CreateSalaryRange(SalariesCreationRequestDto request)
        {
            var response = await _service.CreateAsync(request);
            Response.StatusCode = response.StatusCode ?? StatusCodes.Status201Created;
            return new JsonResult(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet, Route("")]
        public async Task<ActionResult<PagedResponse<SalariesResponseDto>>> GetSalaries(int? page, int? limit)
        {
            var fullPage = page ?? 1;
            var pageSize = limit ?? 10;

            var response = await _service.GetAsync(fullPage, pageSize);
            Response.StatusCode = response.Error != null ? response.Error.ErrorCode : StatusCodes.Status200OK;
            return new JsonResult(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("{id}")]
        public async Task<ActionResult<GenericResponseDto<SalariesResponseDto>>> GetSalaryRange(Guid id)
        {
            var response = await _service.GetSingleAsync(id);
            Response.StatusCode = response.StatusCode ?? StatusCodes.Status201Created;
            return new JsonResult(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut, Route("{id}")]
        public async Task<ActionResult<GenericResponseDto<SalariesResponseDto>>> UpdateSalaryRange(Guid id, SalariesCreationRequestDto request)
        {
            var response = await _service.UpdateAsync(id, request);
            Response.StatusCode = response.StatusCode ?? StatusCodes.Status201Created;
            return new JsonResult(response);
        }
    }
}
