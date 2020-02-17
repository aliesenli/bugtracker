using Bugtracker.Domain;
using System;

namespace Bugtracker.Contracts.Requests
{
    public class UpdateTicketRequest
    {
        public string Name { get; set; }

        public Priority Priority { get; set; }
    }
}
