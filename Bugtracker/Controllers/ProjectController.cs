using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bugtracker.Contracts.Requests;
using Bugtracker.Contracts.Responses;
using Bugtracker.Converters;
using Bugtracker.Domain;
using Bugtracker.Dto;
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
        private readonly IConverter<Project, ProjectDto> _projectToDtoConverter;
        private readonly IConverter<IList<Project>, IList<ProjectDto>> _projectToDtoListConverter;

        public ProjectController(IProjectService projectService, IUriService uriService,
            IConverter<Project, ProjectDto> projectToDtoConverter,
            IConverter<IList<Project>, IList<ProjectDto>> projectToDtoListConverter)
        {
            _projectService = projectService;
            _uriService = uriService;
            _projectToDtoConverter = projectToDtoConverter;
            _projectToDtoListConverter = projectToDtoListConverter;
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
                CreatedOn = project.CreatedOn.ToString(),
                Completion = project.Completion.ToString(),
                Tickets = project.Tickets
            });

            var projectsDto = _projectToDtoListConverter.Convert(projects);

            //return Ok(projectResponse);
            return Ok(projectsDto);
        }

        [HttpGet("api/projects/{projectId}")]
        public async Task<IActionResult> Get([FromRoute]Guid projectId)
        {
            var project = await _projectService.GetProjectByIdAsync(projectId);

            var projectResponse = new ProjectResponse
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CreatedOn = project.CreatedOn.ToString(),
                Completion = project.Completion.ToString(),
                //Tickets = tickets.FindAll(x => x.ProjectId == project.Id)
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
                CreatedOn = project.CreatedOn.ToString(),
                Completion = project.Completion.ToString()
            };

            await _projectService.CreateProjectAsync(project);

            var locationUri = _uriService.GetProjectUri(project.Id.ToString());
            return Created(locationUri, projectResponse);
        }

    }
}