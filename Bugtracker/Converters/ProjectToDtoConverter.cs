using Bugtracker.Domain;
using Bugtracker.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bugtracker.Converters
{
    public class ProjectToDtoConverter : IConverter<Project, ProjectDto>, IConverter<IList<Project>, IList<ProjectDto>>
    {
        public ProjectDto Convert(Project project)
        {
            var projectDto = new ProjectDto
            {
                Id = project.Id,
                Name = project.Name
            };

            foreach (var ticket in project.Tickets)
            {

                var ticketDto = $"Id: {ticket.Id}, Title: {ticket.Title}, Description: {ticket.Description}," +
                    $" Priority: {ticket.Priority}, CreatedAt: {ticket.CreatedAt}, UpdatedAt: {ticket.UpdatedAt}," +
                    $" Status: {ticket.Status}, Assignee: {ticket.Assignee}, AssigneeId: {ticket.AssigneeId}," +
                    $" Submitter: {ticket.Submitter}, SubmitterId: {ticket.SubmitterId}, ProjectId: {ticket.ProjectId}," +
                    $" Project: {ticket.Project}";

                projectDto.Tickets.Add(ticketDto);
            }
            return projectDto;
        }

        public IList<ProjectDto> Convert(IList<Project> projects)
        {
            return projects.Select(cmp =>
            {
                var projectDto = Convert(cmp);
                return projectDto;
            }).ToList();
        }
    }
}
