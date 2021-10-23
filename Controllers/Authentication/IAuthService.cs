using HrMan.Controllers.Authentication.Dtos;
using HrMan.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrMan.Controllers.Authentication
{
    public interface IAuthService
    {
        Task<GenericResponseDto<object>> LoginUser(LoginRequestDto request);

        Task<GenericResponseDto<UserDto>> CreateUserAsync(RegistrationRequestDto requestDto);

        Task<GenericResponseDto<UserDto>> GetCurrentUserAsync(string email);
    }
}
