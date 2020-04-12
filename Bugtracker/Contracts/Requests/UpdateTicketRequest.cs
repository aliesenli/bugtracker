using Bugtracker.Domain;

namespace Bugtracker.Contracts.Requests
{
    public class UpdateTicketRequest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string AssigneeId { get; set; }

        public Status Status { get; set; }

        public Priority Priority { get; set; }
    }
}
