using System.ComponentModel.DataAnnotations;
using Witcherius.BL.DataTransferObjects.Common;

namespace Witcherius.BL.DataTransferObjects.User
{
    public class UserCreateDto : BaseDto
    {
        [Required(ErrorMessage = "User name is required!")]
        [MaxLength(64, ErrorMessage = "Your User name is too long!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Password must be between 6-30 chars!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress(ErrorMessage = "This is not valid Email!")]
        public string Email { get; set; }

        public string Roles { get; set; }
    }
}
