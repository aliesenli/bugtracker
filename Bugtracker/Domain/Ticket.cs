using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bugtracker.Domain
{
    public enum Priority
    {
        Low,
        Medium,
        High
    }

    public class Ticket
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }

        /* TODO
        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }
        */

        public DateTime CreatedAt { get; set; }

        public Priority Priority { get; set; }

    }
}
