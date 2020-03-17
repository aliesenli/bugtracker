using System.Collections.Generic;

namespace Bugtracker.Contracts.Responses
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
