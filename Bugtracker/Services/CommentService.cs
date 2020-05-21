using Bugtracker.Domain;
using Bugtracker.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bugtracker.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public Task<List<Comment>> GetAllAsync(Guid ticketId)
        {
            var comments = _commentRepository.GetAllAsync(ticketId);
            return comments;
        }

        public Task<bool> CreateAsync(Comment comment)
        {
            var created = _commentRepository.CreateAsync(comment);
            return created;
        }

    }
}
