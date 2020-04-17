namespace Bugtracker.Contracts.Requests
{
    public class AssignRoleRequest
    {
        public string User { get; set; }

        public string Role { get; set; }
    }
}
