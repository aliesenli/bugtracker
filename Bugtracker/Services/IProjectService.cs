using Bugtracker.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bugtracker.Services
{
    public interface IProjectService
    {
        Task<List<Project>> GetAllAsync();

        Task<bool> CreateAsync(Project project);

        Task<Project> GetByIdAsync(Guid projectId);

        Task<bool> UpdateAsync(Project projectToUpdate);

        Task<bool> DeleteAsync(Guid projectId);
    }
}
