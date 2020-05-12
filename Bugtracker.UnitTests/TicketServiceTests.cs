using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Bugtracker.Repositories;
using Bugtracker.Services;
using Bugtracker.Domain;

namespace Bugtracker.UnitTests
{
    public class TicketServiceTests
    {
        private readonly TicketService _sut;
        private readonly Mock<ITicketRepository> _ticketRepoMock = new Mock<ITicketRepository>();

        public TicketServiceTests()
        {
            _sut = new TicketService(_ticketRepoMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnTicket_WhenTicketExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var title = "MoqTest";
            var priority = Priority.High;
            var status = Status.Open;
            var ticket = new Ticket
            {
                Id = id,
                Title = title,
                Priority = priority,
                Status = status
            };

            _ticketRepoMock.Setup(x => x.GetByIdAsync(id))
                .ReturnsAsync(ticket);

            // Act
            var ticketResponse = await _sut.GetByIdAsync(id);

            // Assert
            Assert.Equal(id, ticketResponse.Id);
            Assert.Equal(title, ticketResponse.Title);
            Assert.Equal(priority, ticketResponse.Priority);
            Assert.Equal(status, ticketResponse.Status);
        }
    }
}