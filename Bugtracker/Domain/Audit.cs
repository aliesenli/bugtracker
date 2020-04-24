using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bugtracker.Domain
{
    public class Audit
    {
        [Key]
        public int Id { get; set; }

        public string Property { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }

        public DateTime Date { get; set; }

        public Ticket Ticket { get; set; }

        [ForeignKey(nameof(Ticket))]
        public Guid TicketId { get; set; }
    }
}
