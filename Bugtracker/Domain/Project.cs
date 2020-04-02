using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bugtracker.Domain
{
    public class Project
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime Completion { get; set; }

        public virtual IEnumerable<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
