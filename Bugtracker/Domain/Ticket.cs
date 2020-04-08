using System;
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

    public enum Status
    {
        Open,
        Closed,
    }

    public class Ticket
    {
        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Priority Priority { get; set; }

        public Status Status { get; set; }

        public Project Project { get; set; }

        public IdentityUser Assignee { get; set; }

        public IdentityUser Submitter { get; set; }

        [ForeignKey(nameof(Project))]
        public Guid ProjectId { get; set; }

        [ForeignKey(nameof(Assignee))]
        public string AssigneeId { get; set; }

        [ForeignKey(nameof(Submitter))]
        public string SubmitterId { get; set; }
    }
}
