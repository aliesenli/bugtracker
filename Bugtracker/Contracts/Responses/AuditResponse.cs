using System;

namespace Bugtracker.Contracts.Responses
{
    public class AuditResponse
    {
        public int Id { get; set; }

        public string Property { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }

        public string Date { get; set; }
    }
}
