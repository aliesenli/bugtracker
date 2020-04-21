using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Bugtracker.Contracts.Requests;
using Bugtracker.Contracts.Responses;

namespace Bugtracker.IntegrationTests
{
    public class ProjectControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAll_WithoutAnyProjects_ReturnsEmptyResponse()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await TestClient.GetAsync("api/projects");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<IEnumerable<ProjectResponse>>()).Should().BeEmpty();
        }

        [Fact]
        public async Task Get_ReturnsProject_WhenProjectExistsInDatabase()
        {
            // Arrange
            await AuthenticateAsync();

            var date = DateTime.Now;
            var createdProject = await CreateProjectAsync(new CreateProjectRequest
            {
                Name = "Test Project",
                Description = "Test Description",
                Completion = date
            });

            // Act
            var response = await TestClient.GetAsync("api/projects/{projectId}".Replace("{projectId}", createdProject.Id.ToString()));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedProject = await response.Content.ReadAsAsync<ProjectResponse>();
            returnedProject.Id.Should().Be(createdProject.Id);
            returnedProject.Name.Should().Be("Test Project");
            returnedProject.Description.Should().Be("Test Description");
            returnedProject.Completion.Should().Be(date.ToString());
            returnedProject.Tickets.Should().BeEmpty();
        }
    }
}
