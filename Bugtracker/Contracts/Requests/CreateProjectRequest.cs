using System;
using System.ComponentModel.DataAnnotations;

namespace Bugtracker.Contracts.Requests
{
    public class CreateProjectRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Completion { get; set; }
    }
}
