using AutoMapper;
using HrMan.Controllers.Organizations.Dto;
using HrMan.Models.Db;
using HrMan.Models.Dtos;
using HrMan.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace HrMan.Controllers.Organizations
{
    public class OrganizationService : IOrganizationService
    {
        private readonly EmployeeDbContext _context;
        private readonly IMapper _mapper;

        public OrganizationService(EmployeeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GenericResponseDto<OrganizationResponseDto>> CreateAsync(OrganizationCreationRequestDto requestDto)
        {
            var response = new GenericResponseDto<OrganizationResponseDto>();

            var organization = _mapper.Map<Organization>(requestDto);

            try
            {
                _context.Organizations.Add(organization);
                await _context.SaveChangesAsync();

                response.StatusCode = 201;
                response.Result = _mapper.Map<OrganizationResponseDto>(organization);

            }catch (Exception ex)
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

        public Task<GenericResponseDto<bool>> DeleteOrganization(Guid id)
        {
            // TODO: Implement Soft delete
            throw new NotImplementedException();
        }

        public async Task<PagedResponse<OrganizationResponseDto>> GetAsync(int page, int limit)
        {
            var response = new PagedResponse<OrganizationResponseDto>();

            try
            {
                if (page >= 1 && limit >= 1)
                {
                    var organizationQueryable = await Task.Run(() => _context.Organizations.AsQueryable());
                    var organizations = organizationQueryable.ToPagedList(page, limit);

                    response.Result = _mapper.Map<List<OrganizationResponseDto>>(organizations.ToList());
                    response.TotalPages = organizations.PageCount;
                    response.Page = organizations.PageNumber;
                    response.PerPage = organizations.PageSize;
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

        public async Task<GenericResponseDto<OrganizationResponseDto>> GetOrganizationAsync(Guid id)
        {
            var response = new GenericResponseDto<OrganizationResponseDto>();

            var organization = await Task.Run(() => _context.Organizations.FirstOrDefault(o => o.Id == id));

            if (organization != null)
            {
                response.Result = _mapper.Map<OrganizationResponseDto>(organization);
                response.StatusCode = 200;
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

        public async Task<GenericResponseDto<OrganizationResponseDto>> UpdateOrganization(Guid id, OrganizationCreationRequestDto requestDto)
        {
            var response = new GenericResponseDto<OrganizationResponseDto>();

            var organization = await Task.Run(() => _context.Organizations.FirstOrDefault(o => o.Id == id));

            if (organization != null)
            {
                var savedOrganization = _mapper.Map(requestDto, organization);
                await _context.SaveChangesAsync();

                response.Result = _mapper.Map<OrganizationResponseDto>(savedOrganization);
                response.StatusCode = 200;

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
    }
}
