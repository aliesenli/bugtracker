using Bugtracker.Data;
using Bugtracker.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bugtracker.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ProjectRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> CreateAsync(Project project)
        {
            await _applicationDbContext.Projects.AddAsync(project);

            var created = await _applicationDbContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> DeleteAsync(Guid projectId)
        {
            var project = await GetByIdAsync(projectId);

            if (project == null)
                return false;

            _applicationDbContext.Projects.Remove(project);
            var deleted = await _applicationDbContext.SaveChangesAsync();

            return deleted > 0;
        }

        public async Task<List<Project>> GetAllAsync()
        {
            var queryable = _applicationDbContext.Projects
              .Include(p => p.Tickets).ThenInclude(t => t.Submitter)
              .Include(p => p.Tickets).ThenInclude(t => t.Assignee);
            //.AsQueryable();

            return await queryable.ToListAsync();
        }

        public async Task<Project> GetByIdAsync(Guid projectId)
        {
            return await _applicationDbContext.Projects
                .Include(p => p.Tickets).ThenInclude(t => t.Submitter)
                .Include(p => p.Tickets).ThenInclude(t => t.Assignee)
                .SingleOrDefaultAsync(x => x.Id == projectId);
        }

        public async Task<bool> UpdatetAsync(Project projectToUpdate)
        {
            _applicationDbContext.Projects.Update(projectToUpdate);
            var updated = await _applicationDbContext.SaveChangesAsync();

            return updated > 0;
        }
    }
}
