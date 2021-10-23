using System.ComponentModel.DataAnnotations;

namespace HrMan.Controllers.Authentication.Dtos
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "The email is required!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The password is required!")]
        public string Password { get; set; }

    }
}
