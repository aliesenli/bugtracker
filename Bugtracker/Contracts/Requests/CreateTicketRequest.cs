using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bugtracker.Domain;

namespace Bugtracker.Contracts.Requests
{

    public class CreateTicketRequest
    {
        public string Name { get; set; }

        public Priority Priority { get; set; }
    }
}
