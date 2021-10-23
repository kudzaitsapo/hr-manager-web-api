using AutoMapper;
using HrMan.Controllers.Authentication.Dtos;
using HrMan.Models.Db;
using HrMan.Models.Dtos;
using HrMan.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HrMan.Controllers.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly EmployeeDbContext _context;

        public AuthService(UserManager<AppUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            IConfiguration configuration,
            IMapper mapper,
            EmployeeDbContext context

        )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _mapper = mapper;
            _context = context;
        }

        public async Task<GenericResponseDto<UserDto>> CreateUserAsync(RegistrationRequestDto requestDto)
        {
            var response = new GenericResponseDto<UserDto>();
            var existingUser = await _userManager.FindByEmailAsync(requestDto.Email);
            if (existingUser == null)
            {
                var user = _mapper.Map<AppUser>(requestDto);
                user.SecurityStamp = Guid.NewGuid().ToString();
                var result = await _userManager.CreateAsync(user, requestDto.Password);

                if (!result.Succeeded)
                {
                    var error = string.Join<IdentityError>(", ", result.Errors.ToArray());
                    response.Error = new ErrorResponseDto { ErrorCode = 500, Message = "Failed to create user because of the following errors: " + error };
                }
                else
                {
                    response.StatusCode = 200;
                    response.Result = _mapper.Map<UserDto>(user);
                }

            } else
            {
                response.Error = new ErrorResponseDto { ErrorCode = 400, Message = "This email is already registered!" };
                response.StatusCode = 400;
            }

           

            return response;
        }

        public async Task<GenericResponseDto<object>> LoginUser(LoginRequestDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            var response = new GenericResponseDto<object>();

            if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
            {
                var roles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in roles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                response.Result = new 
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    user = _mapper.Map<UserDto>(user),
                    expires = token.ValidTo
                };
                response.StatusCode = 200;

                user.LastLogin = DateTime.Now;

                try
                {
                    await _context.SaveChangesAsync();
                }catch(Exception ex)
                {
                    response.Error = new ErrorResponseDto() { ErrorCode = 500, Message = ex.Message };
                }
                                
                return response;
            }

            response.StatusCode = 400;
            response.Error = new ErrorResponseDto { ErrorCode = 400, Message = "Invalid email or password!" };

            return response;
        }

        public async Task<GenericResponseDto<UserDto>> GetCurrentUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var response = new GenericResponseDto<UserDto>();
            if (user != null)
            {
                response.Result = _mapper.Map<UserDto>(user);
                response.StatusCode = 200;
            } else
            {
                response.Error = new ErrorResponseDto() { ErrorCode = 401, Message = "You are not logged into the system!" };
                response.StatusCode = 401;
            }

            return response;
        }
    }
}
