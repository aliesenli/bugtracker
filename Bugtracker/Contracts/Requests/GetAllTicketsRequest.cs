using Microsoft.AspNetCore.Mvc;

namespace Bugtracker.Contracts.Requests
{
    public class GetAllTicketsRequest
    {
        [FromRoute]
        public string UserId { get; set; }
    }
}
