using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bugtracker.Domain;
using Bugtracker.Repositories;

namespace Bugtracker.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<List<Ticket>> GetAllAsync()
        {
            var tickets = (await _ticketRepository.GetAllAsync()).ToList();
            return tickets;
        }

        public async Task<Ticket> GetByIdAsync(Guid ticketId)
        {
            var ticket = await _ticketRepository.GetByIdAsync(ticketId);
            return ticket;
        }

        public async Task<bool> CreateAsync(Ticket ticketToCreate)
        {
            var ticket = await _ticketRepository.CreateAsync(ticketToCreate);
            return ticket;
        }

        public async Task<bool> DeleteAsync(Guid ticketId)
        {
            var ticket = await _ticketRepository.DeleteAsync(ticketId);
            return ticket;
        }

        public async Task<bool> UpdateAsync(Ticket ticketToUpdate)
        {
            var ticket = await _ticketRepository.UpdateAsync(ticketToUpdate);
            return ticket;
        }

        public async Task<List<Ticket>> GetUsersAsync(string userId)
        {
            var tickets = await _ticketRepository.GetUsersAsync(userId);
            return tickets;
        }

        public async Task<List<Comment>> GetAllCommentsAsync(Guid ticketId)
        {
            var comments = await _ticketRepository.GetAllCommentsAsync(ticketId);
            return comments;
        }

        public async Task<bool> CreateCommentAsync(Comment comment)
        {
            var created = await _ticketRepository.CreateCommentAsync(comment);
            return created;
        }
    }
}
