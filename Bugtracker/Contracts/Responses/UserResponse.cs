using System.Collections.Generic;

namespace Bugtracker.Contracts.Responses
{
    public class UserResponse
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        public IList<string> Role { get; set; }
    }
}
