using System.ComponentModel.DataAnnotations;

namespace Bugtracker.Contracts.Requests
{
    public class UserLoginRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
