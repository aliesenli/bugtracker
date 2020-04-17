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
            var queryable = _applicationDbContext.Tickets
                .Include(t => t.Project)
                .Include(t => t.Assignee)
                .Include(t => t.Submitter)
                .Include(t => t.Audits)
                .AsQueryable();

            return await queryable.ToListAsync();
        }

        public async Task<Ticket> GetTicketByIdAsync(Guid postId)
        {
            return await _applicationDbContext.Tickets
                .Include(t => t.Project)
                .Include(t => t.Assignee)
                .Include(t => t.Submitter)
                .Include(t => t.Audits)
                .SingleOrDefaultAsync(t => t.Id == postId);
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

            var ttu = await _applicationDbContext.Tickets.FirstAsync(t => t.Id == ticketToUpdate.Id);

            foreach (var entry in _applicationDbContext.Entry(ttu).Properties)
            {
                if (entry.IsModified && !(entry.Metadata.Name == "UpdatedAt"))
                {
                    var ticketAudit = new Audit
                    {
                        TicketId = ticketToUpdate.Id,
                        Property = entry.Metadata.Name,
                        OldValue = entry.OriginalValue.ToString(),
                        NewValue = entry.CurrentValue.ToString(),
                        Date = DateTime.Now,
                    };
                    _applicationDbContext.Audits.Add(ticketAudit);
                }
            }

            _applicationDbContext.Tickets.Update(ticketToUpdate);
            var updated = await _applicationDbContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<List<Ticket>> GetUserTicketsAsync(string userId)
        {
            var queryable = _applicationDbContext.Tickets.Where(t => t.Assignee.Id == userId)
                .Include(t => t.Project)
                .Include(t => t.Assignee)
                .AsQueryable();

            return await queryable.ToListAsync();
        }
    }
}
