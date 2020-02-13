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

        public Task<bool> CreateTicketAsync(Ticket post)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTicketAsync(Guid postId)
        {
            throw new NotImplementedException();
        }

        public Task<Ticket> GetTicketByIdAsync(Guid postId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateTicketAsync(Ticket postToUpdate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserOwnsTicketAsync(Guid postId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
