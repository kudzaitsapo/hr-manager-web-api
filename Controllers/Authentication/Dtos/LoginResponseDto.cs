using HrMan.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrMan.Controllers.Authentication.Dtos
{
    public class LoginResponseDto
    {
        public string JwtToken { get; set; }

        public UserDto User { get; set; }

        public DateTime Expires { get; set; }

    }
}
