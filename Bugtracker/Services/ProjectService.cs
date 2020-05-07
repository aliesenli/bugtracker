using Bugtracker.Data;
using Bugtracker.Domain;
using Bugtracker.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bugtracker.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<bool> CreateAsync(Project project)
        {
            var created = await _projectRepository.CreateAsync(project);
            return created;
        }

        public async Task<bool> DeleteAsync(Guid projectId)
        {
            var deleted = await _projectRepository.DeleteAsync(projectId);
            return deleted;
        }

        public async Task<Project> GetByIdAsync(Guid projectId)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);
            return project;
        }

        public async Task<List<Project>> GetAllAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return projects;
        }

        public async Task<bool> UpdateAsync(Project projectToUpdate)
        {
            var updated = await _projectRepository.UpdatetAsync(projectToUpdate);
            return updated;
        }
    }
}
