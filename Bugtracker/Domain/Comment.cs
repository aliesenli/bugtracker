using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Bugtracker.Domain
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; }

        public string Message { get; set; }

        public Ticket Ticket { get; set; }

        public IdentityUser Writer { get; set; }

        [ForeignKey(nameof(Writer))]
        public string WriterId { get; set; }

        [ForeignKey(nameof(Ticket))]
        public Guid TicketId { get; set; }
    }
}
