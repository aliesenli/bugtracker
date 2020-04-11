using Microsoft.AspNetCore.Identity;

namespace Bugtracker.Domain
{
    public class Comment
    {
        public string Id { get; set; }

        public string Message { get; set; }

        public IdentityUser Writer { get; set; }
    }
}
