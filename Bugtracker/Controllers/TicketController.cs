using System;
using System.Threading.Tasks;
using Bugtracker.Contracts.Requests;
using Bugtracker.Contracts.Responses;
using Bugtracker.Domain;
using Bugtracker.Services;
using Microsoft.AspNetCore.Mvc;
using Bugtracker.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Bugtracker.Converters;
using System.Collections.Generic;

namespace Bugtracker.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly IUriService _uriService;
        private readonly IConverter<Ticket, TicketResponse> _ticketToDtoConverter;
        private readonly IConverter<IList<Ticket>, IList<TicketResponse>> _ticketToDtoListConverter;

        public TicketController(
            ITicketService ticketService,
            IUriService uriService,
            IConverter<Ticket, TicketResponse> ticketToDtoConverter,
            IConverter<IList<Ticket>, IList<TicketResponse>> ticketToDtoListConverter)
        {
            _ticketService = ticketService;
            _uriService = uriService;
            _ticketToDtoConverter = ticketToDtoConverter;
            _ticketToDtoListConverter = ticketToDtoListConverter;
        }

        [HttpGet("api/tickets")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllTicketsRequest query)
        {
            var tickets = await _ticketService.GetTicketsAsync();
            var ticketsDto = _ticketToDtoListConverter.Convert(tickets);

            return Ok(ticketsDto);
        }

        [HttpGet("api/tickets/{ticketId}")]
        public async Task<IActionResult> Get([FromRoute]Guid ticketId)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(ticketId);
            var ticketDto = _ticketToDtoConverter.Convert(ticket);

            return Ok(ticketDto);
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
                Status = 0,
                CreatedAt = DateTime.Now,
                Priority = postRequest.Priority,
                ProjectId = postRequest.ProjectId,
            };

            await _ticketService.CreateTicketAsync(ticket);

            var getTicketAfterCreation = await _ticketService.GetTicketByIdAsync(ticket.Id);
            var ticketDto = _ticketToDtoConverter.Convert(getTicketAfterCreation);
            var locationUri = _uriService.GetTicketUri(ticket.Id.ToString());

            return Created(locationUri, ticketDto);
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