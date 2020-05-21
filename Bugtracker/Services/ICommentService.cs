using Bugtracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bugtracker.Services
{
    public interface ICommentService
    {
        Task<List<Comment>> GetAllAsync(Guid ticketId);

        Task<bool> CreateAsync(Comment comment);
    }
}
