using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Bugtracker.Domain;
using Bugtracker.Repositories;
using Bugtracker.Services;

namespace Bugtracker.UnitTests
{
    public class ProjectServiceTests
    {
        private readonly ProjectService _sut;
        private readonly Mock<IProjectRepository> _projectRepoMock = new Mock<IProjectRepository>();

        public ProjectServiceTests()
        {
            _sut = new ProjectService(_projectRepoMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnProject_WhenProjectExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "MoqTest";
            var description = "This is a description";
            var project = new Project
            {
                Id = id,
                Name = name,
                Description = description,
            };

            _projectRepoMock.Setup(x => x.GetByIdAsync(id))
                .ReturnsAsync(project);

            // Act
            var projectResponse = await _sut.GetByIdAsync(id);

            // Assert
            Assert.Equal(id, projectResponse.Id);
            Assert.Equal(name, projectResponse.Name);
            Assert.Equal(description, projectResponse.Description);
        }
    }
}
