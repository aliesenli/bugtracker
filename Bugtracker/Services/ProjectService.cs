using Bugtracker.Data;
using Bugtracker.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bugtracker.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ProjectService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> CreateProjectAsync(Project project)
        {
            await _applicationDbContext.Projects.AddAsync(project);

            var created = await _applicationDbContext.SaveChangesAsync();

            return created > 0;
        }

        public async Task<bool> DeleteProjectAsync(Guid projectId)
        {
            var project = await GetProjectByIdAsync(projectId);

            if (project == null)
                return false;

            _applicationDbContext.Projects.Remove(project);
            var deleted = await _applicationDbContext.SaveChangesAsync();

            return deleted > 0;
        }

        public async Task<Project> GetProjectByIdAsync(Guid projectId)
        {
            return await _applicationDbContext.Projects
                .SingleOrDefaultAsync(x => x.Id == projectId);
        }

        public async Task<List<Project>> GetProjectsAsync()
        {
            var queryable = _applicationDbContext.Projects.AsQueryable();

            return await queryable.ToListAsync();
        }

        public async Task<bool> UpdateProjectAsync(Project projectToUpdate)
        {
            _applicationDbContext.Projects.Update(projectToUpdate);
            var updated = await _applicationDbContext.SaveChangesAsync();

            return updated > 0;
        }
    }
}
