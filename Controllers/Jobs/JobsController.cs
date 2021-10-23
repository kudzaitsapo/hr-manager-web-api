using HrMan.Controllers.Jobs.Dto;
using HrMan.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HrMan.Controllers.Jobs
{
    [Route("api/[controller]")]
    [Authorize()]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IJobService _service;

        public JobsController(IJobService service)
        {
            _service = service;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("")]
        public async Task<ActionResult<GenericResponseDto<JobResponseDto>>> CreateJob(JobCreationRequestDto request)
        {
            var response = await _service.CreateJobAsync(request);
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
        public async Task<ActionResult<PagedResponse<JobResponseDto>>> GetJobs(int? page, int? limit)
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
        public async Task<ActionResult<GenericResponseDto<JobResponseDto>>> GetJob(Guid id)
        {
            var response = await _service.GetJobAsync(id);
            Response.StatusCode = response.StatusCode ?? StatusCodes.Status200OK;
            return new JsonResult(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut, Route("{id}")]
        public async Task<ActionResult<GenericResponseDto<JobResponseDto>>> UpdateJob(Guid id, JobCreationRequestDto request)
        {
            var response = await _service.UpdateJobAsync(id, request);
            Response.StatusCode = response.StatusCode ?? StatusCodes.Status200OK;
            return new JsonResult(response);
        }
    }
}
