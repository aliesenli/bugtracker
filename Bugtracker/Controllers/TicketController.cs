using System;
using System.Linq;
using System.Threading.Tasks;
using Bugtracker.Contracts.Requests;
using Bugtracker.Contracts.Responses;
using Bugtracker.Domain;
using Bugtracker.Services;
using Microsoft.AspNetCore.Mvc;
using Bugtracker.Extensions;

namespace Bugtracker.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly IUriService _uriService;

        public TicketController(ITicketService ticketService, IUriService uriService)
        {
            _ticketService = ticketService;
            _uriService = uriService;
        }

        [HttpGet("api/tickets")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllTicketsRequest query)
        {
            var tickets = await _ticketService.GetTicketsAsync();
            var ticketResponse = tickets.Select(ticket => new TicketResponse
            {
                Id = ticket.Id,
                UserId = ticket.UserId,
                Name = ticket.Name,
                CreatedAt = ticket.CreatedAt.ToString(),
                UpdatedAt = ticket.UpdatedAt.ToString(),
                Priority = ticket.Priority
            });

            return Ok(ticketResponse);
        }

        [HttpGet("api/tickets/{ticketId}")]
        public async Task<IActionResult> Get([FromRoute]Guid ticketId)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(ticketId);
            var ticketResponse = new TicketResponse
            {
                Id = ticket.Id,
                UserId = ticket.UserId,
                Name = ticket.Name,
                CreatedAt = ticket.CreatedAt.ToString(),
                Priority = ticket.Priority
            };

            return Ok(ticketResponse);
        }

        [HttpPost("api/tickets/create")]
        public async Task<IActionResult> Create([FromBody] CreateTicketRequest postRequest)
        {
            var newTicketId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var ticket = new Ticket
            {
                Id = newTicketId,
                Name = postRequest.Name,
                //TODO
                //UserId = HttpContext.GetUserId(),
                UserId = userId.ToString(),
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                Priority = postRequest.Priority,
                ProjectId = postRequest.ProjectId
            };

            var ticketResponse = new TicketResponse
            {
                Id = ticket.Id,
                Name = ticket.Name,
                UserId = ticket.UserId,
                CreatedAt = ticket.CreatedAt.ToString(),
                Priority = ticket.Priority,
                ProjectId = ticket.ProjectId
            };

            await _ticketService.CreateTicketAsync(ticket);

            var locationUri = _uriService.GetPostUri(ticket.Id.ToString());
            return Created(locationUri, ticketResponse);
        }

        [HttpPut("api/ticket/{ticketId}")]
        public async Task<IActionResult> Update([FromRoute]Guid ticketId, [FromBody] UpdateTicketRequest request)
        {
            //TODO
            //var userOwnsPost = await _ticketService.UserOwnsPostAsync(ticketId, HttpContext.GetUserId());

            var ticket = await _ticketService.GetTicketByIdAsync(ticketId);
            ticket.Name = request.Name;
            ticket.UpdatedAt = DateTime.UtcNow;
            ticket.Priority = request.Priority;

            var updated = await _ticketService.UpdateTicketAsync(ticket);

            var ticketResponse = new TicketResponse
            {
                Id = ticket.Id,
                UserId = ticket.UserId,
                Name = ticket.Name,
                CreatedAt = ticket.CreatedAt.ToString(),
                UpdatedAt = ticket.UpdatedAt.ToString(),
                Priority = ticket.Priority
            };

            if (updated)
                return Ok(ticketResponse);

            return NotFound();
        }

        [HttpDelete("api/tickets/{ticketId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid ticketId)
        {
            //TODO
            //var userOwnsPost = await _ticketService.UserOwnsTicketAsync(postId);
            var deleted = await _ticketService.DeleteTicketAsync(ticketId);

            if (deleted)
                return NoContent();

            return NotFound();
        }
    }

}