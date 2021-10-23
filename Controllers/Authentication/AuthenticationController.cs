using HrMan.Controllers.Authentication.Dtos;
using HrMan.Models.Dtos;
using HrMan.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost, Route("Login")]
        public async Task<ActionResult<GenericResponseDto<object>>> Login(LoginRequestDto loginRequest)
        {
            var response = await _authService.LoginUser(loginRequest);
            Response.StatusCode = response.StatusCode ?? StatusCodes.Status200OK;
            return new JsonResult(response);
        }

        [HttpPost, Route("Register")]
        public async Task<ActionResult<GenericResponseDto<UserDto>>> Register(RegistrationRequestDto registrationRequest)
        {
            var response = await _authService.CreateUserAsync(registrationRequest);
            Response.StatusCode = response.StatusCode ?? StatusCodes.Status200OK;
            return new JsonResult(response);
        }

        [HttpGet, Route("CurrentProfile")]
        [Authorize()]
        public async Task<ActionResult<GenericResponseDto<UserDto>>> CurrentProfile()
        {
            var context = new HttpContextAccessor();
            var email = context.HttpContext.User.Identity.Name;

            var response = await _authService.GetCurrentUserAsync(email);
            Response.StatusCode = response.StatusCode ?? StatusCodes.Status200OK;
            return new JsonResult(response);
        }
    }
}
