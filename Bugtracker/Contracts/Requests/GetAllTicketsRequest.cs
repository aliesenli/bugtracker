using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bugtracker.Contracts.Requests
{
    public class GetAllTicketsRequest
    {
        [FromQuery(Name = "userId")]
        public string UserId { get; set; }
    }
}
