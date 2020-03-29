using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

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

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Priority Priority { get; set; }

        public string ProjectId { get; set; }

        /* TODO
        [ForeignKey(nameof(UserId))]
        public IdentityUser Assignee { get; set; }
        */
    }
}
