using Bugtracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bugtracker.Dto
{
    public class ProjectDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CreatedOn { get; set; }

        public string Completion { get; set; }

        public ICollection<string> Tickets { get; set; } = new HashSet<string>();
    }
}
