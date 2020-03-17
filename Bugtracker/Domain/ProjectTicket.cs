using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bugtracker.Domain
{
    public class ProjectTicket
    {
        [ForeignKey(nameof(TicketId))]
        public virtual Ticket Ticket { get; set; }

        public Guid TicketId { get; set; }

        public virtual Project Project { get; set; }

        public Guid ProjectId { get; set; }
    }
}
