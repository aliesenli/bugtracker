using Bugtracker.Data;
using Bugtracker.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bugtracker.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public TicketRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Ticket>> GetAllAsync()
        {
            var queryable = _applicationDbContext.Tickets
                .Include(t => t.Project)
                .Include(t => t.Assignee)
                .Include(t => t.Submitter)
                .Include(t => t.Audits)
                .AsQueryable();

            foreach (var item in queryable)
            {
                item.Audits = item.Audits
                    .OrderByDescending(a => a.Date.Date)
                    .ThenByDescending(a => a.Date.Hour)
                    .ThenByDescending(a => a.Date.Minute)
                    .ToList();
            }

            return await queryable.ToListAsync();
        }

        public async Task<Ticket> GetByIdAsync(Guid ticketId)
        {
            var queryable = _applicationDbContext.Tickets
                .Include(t => t.Project)
                .Include(t => t.Assignee)
                .Include(t => t.Submitter)
                .Include(t => t.Audits)
                .SingleOrDefaultAsync(t => t.Id == ticketId);

            queryable.Result.Audits = queryable.Result.Audits
                .OrderByDescending(a => a.Date.Date)
                .ThenByDescending(a => a.Date.Hour)
                .ToList();

            return await queryable;
        }

        public async Task<bool> CreateAsync(Ticket ticket)
        {
            await _applicationDbContext.Tickets.AddAsync(ticket);

            var created = await _applicationDbContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> DeleteAsync(Guid ticketId)
        {
            var ticket = await GetByIdAsync(ticketId);

            if (ticket == null)
                return false;

            _applicationDbContext.Tickets.Remove(ticket);
            var deleted = await _applicationDbContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> UpdateAsync(Ticket ticketToUpdate)
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

        public async Task<List<Ticket>> GetUsersAsync(string userId)
        {
            var queryable = _applicationDbContext.Tickets.Where(t => t.Assignee.Id == userId)
                .Include(t => t.Project)
                .Include(t => t.Assignee)
                .Include(t => t.Submitter)
                .AsQueryable();

            return await queryable.ToListAsync();
        }
    }
}
