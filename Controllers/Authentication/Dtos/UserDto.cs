using System;

namespace HrMan.Controllers.Authentication.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }

        public string Firstname { get; set; }

        public string Middlename { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string MobileNumber { get; set; }

        public DateTime LastLogin { get; set; }

    }
}
