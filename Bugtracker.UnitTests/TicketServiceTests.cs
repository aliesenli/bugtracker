using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Bugtracker.Repositories;
using Bugtracker.Services;
using Bugtracker.Domain;
using System.Collections.Generic;
using System.Linq;

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

        [Fact]
        public async Task DeleteByIdAsync_ShouldReturnTrue_WhenTicketDeleted()
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

            _ticketRepoMock.Setup(x => x.DeleteAsync(ticket.Id))
                .ReturnsAsync(true);

            // Act
            var ticketResponse = await _sut.DeleteAsync(ticket.Id);

            // Assert
            Assert.True(ticketResponse);
        }

        [Fact]
        public async Task UpdateTicket_ShouldReturnTrue_WhenTicketUpdated()
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

            var updatedTitle = "Updated Title";
            var updatedPriority = Priority.Medium;
            var updatedStatus = Status.Closed;

            _ticketRepoMock.Setup(x => x.GetByIdAsync(ticket.Id))
                .ReturnsAsync(ticket);

            _ticketRepoMock.Setup(x => x.UpdateAsync(ticket))
                .ReturnsAsync(true);

            // Act
            var ticketResponse = await _sut.GetByIdAsync(id);

            ticketResponse.Title = updatedTitle;
            ticketResponse.Priority = updatedPriority;
            ticketResponse.Status = updatedStatus;

            var ticketAfterUpdateResponse = await _sut.GetByIdAsync(id);
            var updateTicketResponse = await _sut.UpdateAsync(ticket);

            // Assert
            Assert.True(updateTicketResponse);
            Assert.Equal(updatedTitle, ticketAfterUpdateResponse.Title);
            Assert.Equal(updatedPriority, ticketAfterUpdateResponse.Priority);
            Assert.Equal(updatedStatus, ticketAfterUpdateResponse.Status);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnComment_WhenCommentExists()
        {
            // Arrange
            var commentId = Guid.NewGuid();
            var ticketId = Guid.NewGuid();
            var message = "This is a comment";
            var comment = new Comment
            {
                Id = commentId,
                Message = message,
                TicketId = ticketId,
                WriterId = "1"
            };

            _ticketRepoMock.Setup(x => x.GetCommentByIdAsync(commentId))
               .ReturnsAsync(comment);

            // Act
            var commentResponse = await _sut.GetCommentByIdAsync(commentId);

            // Assert
            Assert.NotNull(commentResponse);
            Assert.Equal(comment.Id, commentResponse.Id);
            Assert.Equal(comment.Message, commentResponse.Message);
            Assert.Equal(comment.TicketId, commentResponse.TicketId);
            Assert.Equal(comment.WriterId, commentResponse.WriterId);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnList_WhenTicketsExists()
        {
            // Arrange
            List<Ticket> allTickets = new List<Ticket>();

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

            var id2 = Guid.NewGuid();
            var title2 = "MoqTest2";
            var priority2 = Priority.High;
            var status2 = Status.Open;
            var ticket2 = new Ticket
            {
                Id = id2,
                Title = title2,
                Priority = priority2,
                Status = status2
            };

            allTickets.Add(ticket);
            allTickets.Add(ticket2);

            _ticketRepoMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(allTickets);

            // Act
            var ticketResponse = await _sut.GetAllAsync();

            // Assert
            Assert.NotEmpty(ticketResponse);
            Assert.NotNull(ticketResponse);
            Assert.Contains(ticketResponse, item => item == ticket);
            Assert.Contains(ticketResponse, item => item == ticket2);
        }
    }
}