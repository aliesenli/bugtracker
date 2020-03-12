using Microsoft.AspNetCore.Mvc;

namespace Bugtracker.Contracts.Requests
{
    public class GetAllTicketsRequest
    {
        [FromQuery(Name = "userId")]
        public string UserId { get; set; }
    }
}
