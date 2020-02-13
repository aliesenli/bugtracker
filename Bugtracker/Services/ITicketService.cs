using Bugtracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bugtracker.Services
{
    public interface ITicketService
    {
        Task<List<Ticket>> GetTicketsAsync();
        Task<bool> CreateTicketAsync(Ticket post);

        Task<Ticket> GetTicketByIdAsync(Guid postId);

        Task<bool> UpdateTicketAsync(Ticket postToUpdate);

        Task<bool> DeleteTicketAsync(Guid postId);

        Task<bool> UserOwnsTicketAsync(Guid postId, string userId);
    }
}
