using Bugtracker.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bugtracker.Repositories
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllAsync();

        Task<bool> CreateAsync(Project project);

        Task<Project> GetByIdAsync(Guid projectId);

        Task<bool> UpdatetAsync(Project projectToUpdate);

        Task<bool> DeleteAsync(Guid projectId);
    }
}
