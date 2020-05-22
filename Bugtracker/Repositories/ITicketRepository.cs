using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bugtracker.Domain;

namespace Bugtracker.Repositories
{
    public interface ITicketRepository
    {
        Task<IEnumerable<Ticket>> GetAllAsync();

        Task<Ticket> GetByIdAsync(Guid ticketId);

        Task<bool> CreateAsync(Ticket ticket);

        Task<bool> DeleteAsync(Guid ticketId);

        Task<bool> UpdateAsync(Ticket ticketToUpdate);

        Task<List<Ticket>> GetUsersAsync(string userId);

        Task<List<Comment>> GetAllCommentsAsync(Guid ticketId);

        Task<bool> CreateCommentAsync(Comment comment);
    }
}
