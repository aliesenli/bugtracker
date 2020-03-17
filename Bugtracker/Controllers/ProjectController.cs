using System;
using System.Linq;
using System.Threading.Tasks;
using Bugtracker.Contracts.Requests;
using Bugtracker.Contracts.Responses;
using Bugtracker.Domain;
using Bugtracker.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bugtracker.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IUriService _uriService;
        private readonly ITicketService _ticketService;

        public ProjectController(IProjectService projectService, IUriService uriService, ITicketService ticketService)
        {
            _projectService = projectService;
            _uriService = uriService;
            _ticketService = ticketService;
        }

        [HttpGet("api/projects")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllProjectsRequest query)
        {
            var tickets = await _ticketService.GetTicketsAsync();
            var projects = await _projectService.GetProjectsAsync();

            var projectResponse = projects.Select(project => new ProjectResponse
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CreatedOn = project.CreatedOn,
                Completion = project.Completion,
                Tickets = tickets.FindAll(x => x.ProjectId == project.Id)
            });

            return Ok(projectResponse);
        }

        [HttpGet("api/projects/{projectId}")]
        public async Task<IActionResult> Get([FromRoute]Guid projectId)
        {
            var tickets = await _ticketService.GetTicketsAsync();
            var project = await _projectService.GetProjectByIdAsync(projectId);
            var projectResponse = new ProjectResponse
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CreatedOn = project.CreatedOn,
                Completion = project.Completion,
                Tickets = tickets.FindAll(x => x.ProjectId == project.Id)
            };

            return Ok(projectResponse);
        }

        [HttpPost("api/projects/create")]
        public async Task<IActionResult> Create([FromBody] CreateProjectRequest postRequest)
        {
            var newProjectId = Guid.NewGuid();

            var project = new Project
            {
                Id = newProjectId,
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