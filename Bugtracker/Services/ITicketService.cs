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

        Task<bool> CreateTicketAsync(Ticket ticket);

        Task<Ticket> GetTicketByIdAsync(Guid ticketId);

        Task<bool> UpdateTicketAsync(Ticket ticketToUpdate);

        Task<bool> DeleteTicketAsync(Guid ticketId);

        Task<bool> UserOwnsTicketAsync(Guid ticketId, string userId);
    }
}
