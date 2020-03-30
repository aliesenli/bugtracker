using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
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

        public Guid ProjectId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public Project Project { get; set; }

        public string SubmitterId { get; set; }

        public string AssigneeId { get; set; }

        [ForeignKey(nameof(SubmitterId))]
        public IdentityUser Submitter { get; set; }

        [ForeignKey(nameof(AssigneeId))]
        public IdentityUser Assignee { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Priority Priority { get; set; }

        public Status Status { get; set; }
    }
}
