using Bugtracker.Contracts.Responses;
using FluentAssertions;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Bugtracker.IntegrationTests
{
    public class UserControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAll_WithoutAnyUsers_ReturnsEmptyResponse()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await TestClient.GetAsync("api/users");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<IEnumerable<UserResponse>>()).Should().NotBeEmpty();
            //----> Should not be empty beacause I need to be Authenticated to use the getUsers api endpoint.
        }
    }
}
