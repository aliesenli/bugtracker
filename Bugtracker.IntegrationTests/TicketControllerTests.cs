using Bugtracker.Contracts.Responses;
using FluentAssertions;
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

        /*
        [Fact]
        public async Task Get_ReturnsTicket_WhenTicketExistsInTheDatabase()
        {
            // Arrange
            await AuthenticateAsync();
            var testProjectId = Guid.NewGuid();
            var createdTicket = await CreateTicketAsync(new CreateTicketRequest
            {
                Title = "Test Title",
                Description = "Test Description",
                Priority = 0,
                ProjectId = testProjectId,
            });

            // Act
            var response = await TestClient.GetAsync("api/tickets/{ticketId}".Replace("{ticketId}", createdTicket.Id.ToString()));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedPost = await response.Content.ReadAsAsync<TicketResponse>();
            returnedPost.Id.Should().Be(createdTicket.Id);
            returnedPost.Title.Should().Be("Test Title");
            returnedPost.Description.Should().Be("Test Description");
        }
        */
    }
}
