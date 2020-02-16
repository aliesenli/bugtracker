using System;
using System.Threading.Tasks;
using Bugtracker.Contracts.Requests;
using Bugtracker.Data;
using Bugtracker.Domain;
using Bugtracker.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

        public ApplicationDbContext DbContext { get; }

        [HttpGet("api/tickets")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllTicketsRequest query)
        {
            var tickets = await _ticketService.GetTicketsAsync();

            return Ok(tickets);
        }

        [HttpGet("api/tickets/{ticketId}")]
        public async Task<IActionResult> Get([FromRoute]Guid ticketId)
        {
            var tickets = await _ticketService.GetTicketByIdAsync(ticketId);

            return Ok(tickets);
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
                CreatedAt = DateTime.UtcNow,
                Priority = postRequest.Priority
            };

            await _ticketService.CreateTicketAsync(ticket);

            var locationUri = _uriService.GetPostUri(ticket.Id.ToString());
            return Created(locationUri, ticket);
        }
    }

}