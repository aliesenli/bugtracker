using Bugtracker.Contracts.Responses;
using Bugtracker.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Bugtracker.Converters
{
    public class ProjectToDtoConverter : IConverter<Project, ProjectResponse>, IConverter<IList<Project>, IList<ProjectResponse>>
    {
        public ProjectResponse Convert(Project project)
        {
            var projectDto = new ProjectResponse
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CreatedOn = project.CreatedOn.ToString(),
                Completion = project.Completion.ToString()
            };

            foreach (var ticket in project.Tickets)
            {
                var ticketDtos = new TicketResponse
                {
                    Id = ticket.Id,
                    Title = ticket.Title,
                    Description = ticket.Description,
                    Status = ticket.Status.ToString(),
                    CreatedOn = ticket.CreatedAt.ToString(),
                    UpdatedOn = ticket.UpdatedAt.ToString(),
                    Priority = ticket.Priority.ToString(),
                    Project = ticket.Project.Name,
                    Submitter = ticket.Submitter.UserName,
                    Assignee = ticket.Assignee == null ? "" : ticket.Assignee.UserName
                };

                projectDto.Tickets.Add(ticketDtos);
            }

            return projectDto;
        }

        public IList<ProjectResponse> Convert(IList<Project> projects)
        {
            return projects.Select(cmp =>
            {
                var projectDto = Convert(cmp);
                return projectDto;
            }).ToList();
        }
    }
}
