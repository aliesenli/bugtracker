using System;
using System.Linq;
using System.Threading.Tasks;
using Bugtracker.Contracts.Requests;
using Bugtracker.Contracts.Responses;
using Bugtracker.Domain;
using Bugtracker.Services;
using Microsoft.AspNetCore.Mvc;
using Bugtracker.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Bugtracker.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
                Title = ticket.Title,
                Description = ticket.Description,
                Priority = ticket.Priority.ToString(),
                CreatedOn = ticket.CreatedAt.ToString(),
                UpdatedOn = ticket.UpdatedAt.ToString(),
                Project = ticket.Project.Name,
                Submitter = ticket.Submitter.UserName,
                Assignee = ticket.Assignee.UserName
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
                Title = ticket.Title,
                Description = ticket.Description,
                Priority = ticket.Priority.ToString(),
                CreatedOn = ticket.CreatedAt.ToString(),
                UpdatedOn = ticket.UpdatedAt.ToString(),
                Project = ticket.Project.Name,
                Submitter = ticket.Submitter.UserName,
                Assignee = ticket.Assignee.UserName
            };

            return Ok(ticketResponse);
        }

        [HttpPost("api/tickets/create")]
        public async Task<IActionResult> Create([FromBody] CreateTicketRequest postRequest)
        {
            var newTicketId = Guid.NewGuid();

            var ticket = new Ticket
            {
                Id = newTicketId,
                Title = postRequest.Name,
                Description = postRequest.Description,
                SubmitterId = HttpContext.GetUserId(),
                AssigneeId = postRequest.AssigneeId,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                Priority = postRequest.Priority,
                Status = 0, // Enum 0 means status "Open"
                ProjectId = postRequest.ProjectId,
            };

            var ticketResponse = new TicketResponse
            {
                Id = ticket.Id,
                Title = ticket.Title,
                //SubmitterId = ticket.SubmitterId,
                CreatedOn = ticket.CreatedAt.ToString(),
                //Priority = ticket.Priority,
                //ProjectId = ticket.ProjectId
            };

            await _ticketService.CreateTicketAsync(ticket);


            var locationUri = _uriService.GetPostUri(ticket.Id.ToString());
            return Created(locationUri, ticketResponse);
        }

        [HttpPut("api/ticket/{ticketId}")]
        public async Task<IActionResult> Update([FromRoute]Guid ticketId, [FromBody] UpdateTicketRequest request)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(ticketId);
            ticket.Title = request.Name;
            ticket.UpdatedAt = DateTime.UtcNow;
            ticket.Priority = request.Priority;

            var updated = await _ticketService.UpdateTicketAsync(ticket);

            var ticketResponse = new TicketResponse
            {
                Id = ticket.Id,
                //  SubmitterId = ticket.SubmitterId,
                Title = ticket.Title,
                CreatedOn = ticket.CreatedAt.ToString(),
                UpdatedOn = ticket.UpdatedAt.ToString(),
                //  Priority = ticket.Priority
            };

            if (updated)
                return Ok(ticketResponse);

            return NotFound();
        }

        [HttpDelete("api/tickets/{ticketId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid ticketId)
        {
            var deleted = await _ticketService.DeleteTicketAsync(ticketId);

            if (deleted)
                return NoContent();

            return NotFound();
        }
    }

}