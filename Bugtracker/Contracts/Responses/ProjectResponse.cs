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

        public string CreatedOn { get; set; }

        public string Completion { get; set; }

        public List<Ticket> Tickets { get; set; }
    }
}
