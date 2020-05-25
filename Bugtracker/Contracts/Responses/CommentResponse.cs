using System;

namespace Bugtracker.Contracts.Responses
{
    public class CommentResponse
    {
        public Guid Id { get; set; }

        public string Writer { get; set; }

        public string Message { get; set; }
    }
}
