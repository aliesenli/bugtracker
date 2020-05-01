using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bugtracker.Data;
using Bugtracker.Domain;
using Bugtracker.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bugtracker.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<List<Ticket>> GetTicketsAsync()
        {
            var tickets = (await _ticketRepository.GetAllAsync()).ToList();
            return tickets;
        }

        public async Task<Ticket> GetTicketByIdAsync(Guid ticketId)
        {
            var ticket = (await _ticketRepository.GetByIdAsync(ticketId));
            return ticket;
        }

        public async Task<bool> CreateTicketAsync(Ticket ticketToCreate)
        {
            var ticket = (await _ticketRepository.CreateAsync(ticketToCreate));
            return ticket;
        }

        public async Task<bool> DeleteTicketAsync(Guid ticketId)
        {
            var ticket = (await _ticketRepository.DeleteAsync(ticketId));
            return ticket;
        }

        public async Task<bool> UpdateTicketAsync(Ticket ticketToUpdate)
        {
            var ticket = (await _ticketRepository.UpdateAsync(ticketToUpdate));
            return ticket;
        }

        public async Task<List<Ticket>> GetUserTicketsAsync(string userId)
        {
            var tickets = (await _ticketRepository.GetUsersAsync(userId));
            return tickets;
        }
    }
}
