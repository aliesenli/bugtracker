using Microsoft.AspNetCore.Mvc;

namespace Bugtracker.Contracts.Requests
{
    public class GetAllProjectsRequest
    {
        [FromQuery(Name = "userId")]
        public string UserId { get; set; }
    }
}
