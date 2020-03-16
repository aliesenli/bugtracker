using Bugtracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bugtracker.Services
{
    public interface IProjectService
    {
        Task<List<Project>> GetProjectsAsync();

        Task<bool> CreateProjectAsync(Project project);

        Task<Project> GetProjectByIdAsync(Guid projectId);

        Task<bool> UpdateProjectAsync(Project projectToUpdate);

        Task<bool> DeleteProjectAsync(Guid projectId);
    }
}
