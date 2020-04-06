using System;

namespace Bugtracker.Contracts.Responses
{
    public class TicketResponse
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Priority { get; set; }

        public string CreatedOn { get; set; }

        public string UpdatedOn { get; set; }

        public string Project { get; set; }

        public string Submitter { get; set; }

        public string Assignee { get; set; }

        public string Status { get; set; }
    }
}
