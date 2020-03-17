using Bugtracker.Domain;
using System;
using System.Collections.Generic;

namespace Bugtracker.Contracts.Responses
{
    public class ProjectResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime Completion { get; set; }

        public List<Ticket> Tickets { get; set; }
    }
}
