using HrMan.Controllers.Organizations.Dto;
using HrMan.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HrMan.Controllers.Organizations
{
    [Route("api/[controller]")]
    [Authorize()]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationsController(IOrganizationService service)
        {
            _organizationService = service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet, Route("")]
        public async Task<ActionResult<PagedResponse<OrganizationResponseDto>>> GetOrganizations(int? page, int? limit)
        {
            var fullPage = page ?? 1;
            var pageSize = limit ?? 10;
            var response = await _organizationService.GetAsync(fullPage, pageSize);
            Response.StatusCode = StatusCodes.Status200OK;
            return new JsonResult(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("{id}")]
        public async Task<ActionResult<GenericResponseDto<OrganizationResponseDto>>> GetSingleOrganization(Guid id)
        {
            var response = await _organizationService.GetOrganizationAsync(id);
            Response.StatusCode = response.StatusCode ?? StatusCodes.Status200OK;
            return new JsonResult(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("")]
        public async Task<ActionResult<GenericResponseDto<OrganizationResponseDto>>> CreateOrganization(OrganizationCreationRequestDto request)
        {
            var response = await _organizationService.CreateAsync(request);
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
        public async Task<ActionResult<GenericResponseDto<OrganizationResponseDto>>> UpdateOrganization(Guid id, OrganizationCreationRequestDto request)
        {
            var response = await _organizationService.UpdateOrganization(id, request);
            Response.StatusCode = response.StatusCode ?? StatusCodes.Status200OK;
            return new JsonResult(response);
        }

    }
}
