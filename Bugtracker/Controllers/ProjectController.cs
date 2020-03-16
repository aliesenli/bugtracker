using System;
using System.Linq;
using System.Threading.Tasks;
using Bugtracker.Contracts.Requests;
using Bugtracker.Contracts.Responses;
using Bugtracker.Domain;
using Bugtracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bugtracker.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IUriService _uriService;

        public ProjectController(IProjectService projectService, IUriService uriService)
        {
            _projectService = projectService;
            _uriService = uriService;
        }

        [HttpGet("api/projects")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllProjectsRequest query)
        {
            var projects = await _projectService.GetProjectsAsync();
            var projectResponse = projects.Select(project => new ProjectResponse
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CreatedOn = project.CreatedOn,
                Completion = project.Completion
            });

            return Ok(projectResponse);
        }

        [HttpPost("api/projects/create")]
        public async Task<IActionResult> Create([FromBody] CreateProjectRequest postRequest)
        {
            Guid projectId = new Guid();

            var project = new Project
            {
                Id = projectId,
                Name = postRequest.Name,
                Description = postRequest.Description,
                CreatedOn = DateTime.Now,
                Completion = postRequest.Completion
            };

            var projectResponse = new ProjectResponse
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CreatedOn = project.CreatedOn,
                Completion = project.Completion
            };

            await _projectService.CreateProjectAsync(project);

            var locationUri = _uriService.GetProjectUri(project.Id.ToString());
            return Created(locationUri, projectResponse);
        }
    }
}