using HrMan.Controllers.Organizations.Dto;
using HrMan.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrMan.Controllers.Organizations
{
    public interface IOrganizationService
    {
        Task<GenericResponseDto<OrganizationResponseDto>> CreateAsync(OrganizationCreationRequestDto requestDto);

        Task<PagedResponse<OrganizationResponseDto>> GetAsync(int page, int limit);

        Task<GenericResponseDto<OrganizationResponseDto>> GetOrganizationAsync(Guid id);

        Task<GenericResponseDto<OrganizationResponseDto>> UpdateOrganization(Guid id, OrganizationCreationRequestDto requestDto);

        Task<GenericResponseDto<bool>> DeleteOrganization(Guid id);
    }
}
