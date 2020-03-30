using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bugtracker.Data;
using Bugtracker.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bugtracker.Services
{
    public class TicketService : ITicketService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public TicketService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<Ticket>> GetTicketsAsync()
        {
            var queryable = _applicationDbContext.Tickets.AsQueryable();

            return await queryable.ToListAsync();
        }

        public async Task<Ticket> GetTicketByIdAsync(Guid postId)
        {
            return await _applicationDbContext.Tickets
                 .SingleOrDefaultAsync(x => x.Id == postId);
        }

        public async Task<bool> CreateTicketAsync(Ticket post)
        {
            await _applicationDbContext.Tickets.AddAsync(post);

            var created = await _applicationDbContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> DeleteTicketAsync(Guid ticketId)
        {
            var post = await GetTicketByIdAsync(ticketId);

            if (post == null)
                return false;

            _applicationDbContext.Tickets.Remove(post);
            var deleted = await _applicationDbContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> UpdateTicketAsync(Ticket ticketToUpdate)
        {
            _applicationDbContext.Tickets.Update(ticketToUpdate);
            var updated = await _applicationDbContext.SaveChangesAsync();
            return updated > 0;
        }

        public Task<bool> UserOwnsTicketAsync(Guid ticketId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
