using Bugtracker.Contracts.Requests;
using Bugtracker.Contracts.Responses;
using Bugtracker.Domain;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Bugtracker.IntegrationTests
{
    public class TicketControllerTests : IntegrationTest
    {

        [Fact]
        public async Task GetAll_WithoutAnyTickets_ReturnsEmptyResponse()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await TestClient.GetAsync("api/tickets");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<IEnumerable<TicketResponse>>()).Should().BeEmpty();
        }


        [Fact]
        public async Task Get_ReturnsTicket_WhenTicketExistsInTheDatabase()
        {
            // Arrange
            await AuthenticateWithRoleAsync();
            var createdProject = await CreateProjectAsync(new CreateProjectRequest
            {
                Name = "Test Project",
                Description = "Test Description",
                Completion = DateTime.Now
            });
            var createdTicket = await CreateTicketAsync(new CreateTicketRequest
            {
                Title = "Test Title",
                Description = "Test Description",
                Priority = 0,
                ProjectId = createdProject.Id,
                AssigneeId = ""
            });

            // Act
            var response = await TestClient.GetAsync("api/tickets/{ticketId}".Replace("{ticketId}", createdTicket.Id.ToString()));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedTicket = await response.Content.ReadAsAsync<TicketResponse>();
            returnedTicket.Id.Should().Be(createdTicket.Id);
            returnedTicket.Title.Should().Be("Test Title");
            returnedTicket.Description.Should().Be("Test Description");
            returnedTicket.Status.Should().Be(Status.Open.ToString());
            returnedTicket.Assignee.Should().BeNullOrEmpty();
            returnedTicket.Audits.Should().BeNullOrEmpty();
        }
    }
}
