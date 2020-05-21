using Bugtracker.Data;
using Bugtracker.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bugtracker.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public CommentRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<Comment>> GetAllAsync(Guid ticketId)
        {
            var queryable = _applicationDbContext.Comments.Where(c => c.TicketId == ticketId);

            return await queryable.ToListAsync();
        }

        public async Task<bool> CreateAsync(Comment comment)
        {
            await _applicationDbContext.Comments.AddAsync(comment);

            var created = await _applicationDbContext.SaveChangesAsync();
            return created > 0;
        }

    }
}
