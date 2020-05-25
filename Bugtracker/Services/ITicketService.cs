using Bugtracker.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bugtracker.Services
{
    public interface ITicketService
    {
        Task<List<Ticket>> GetAllAsync();

        Task<bool> CreateAsync(Ticket ticket);

        Task<Ticket> GetByIdAsync(Guid ticketId);

        Task<bool> UpdateAsync(Ticket ticketToUpdate);

        Task<bool> DeleteAsync(Guid ticketId);

        Task<List<Ticket>> GetUsersAsync(string userId);

        Task<List<Comment>> GetAllCommentsAsync(Guid ticketId);

        Task<Comment> GetCommentByIdAsync(Guid commentId);

        Task<bool> CreateCommentAsync(Comment comment);
    }
}
