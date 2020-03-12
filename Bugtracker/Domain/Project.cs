using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bugtracker.Domain
{
    public class Project
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime Completion { get; set; }
        public List<Ticket> Tickets { get; set; }
        //TODO: Project Team Assignments
        //public ICollection<IdentityUser> IdentityUsers { get; set; }
    }
}
