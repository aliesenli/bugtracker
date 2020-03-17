namespace Bugtracker.Contracts.Requests
{
    public class UserLoginRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
