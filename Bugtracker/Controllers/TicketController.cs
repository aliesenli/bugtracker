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
using Microsoft.AspNetCore.Http;

namespace Bugtracker.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly IUserService _userService;
        private readonly IUriService _uriService;
        private readonly IConverter<Ticket, TicketResponse> _ticketToDtoConverter;
        private readonly IConverter<IList<Ticket>, IList<TicketResponse>> _ticketToDtoListConverter;
        private readonly IConverter<IList<Ticket>, IList<TicketResponse>> _userTicketToDtoListConverter;

        public TicketController(
            ITicketService ticketService,
            IUriService uriService,
            IUserService userService,
            IConverter<Ticket, TicketResponse> ticketToDtoConverter,
            IConverter<IList<Ticket>, IList<TicketResponse>> ticketToDtoListConverter,
            IConverter<IList<Ticket>, IList<TicketResponse>> userTicketToDtoListConverter)
        {
            _ticketService = ticketService;
            _uriService = uriService;
            _userService = userService;
            _ticketToDtoConverter = ticketToDtoConverter;
            _ticketToDtoListConverter = ticketToDtoListConverter;
            _userTicketToDtoListConverter = userTicketToDtoListConverter;
        }

        [HttpGet("api/tickets/user/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllFromUser([FromRoute] GetAllTicketsRequest query)
        {
            var tickets = await _ticketService.GetUsersAsync(query.UserId);
            var ticketsDto = _userTicketToDtoListConverter.Convert(tickets);

            return Ok(ticketsDto);
        }

        [HttpGet("api/tickets")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var tickets = await _ticketService.GetAllAsync();
            var ticketsDto = _ticketToDtoListConverter.Convert(tickets);

            return Ok(ticketsDto);
        }

        [HttpGet("api/tickets/{ticketId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute]Guid ticketId)
        {
            var ticket = await _ticketService.GetByIdAsync(ticketId);
            var ticketDto = _ticketToDtoConverter.Convert(ticket);

            return Ok(ticketDto);
        }

        [HttpPost("api/tickets/create")]
        [Authorize(Roles = "Admin, Manager")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateTicketRequest postRequest)
        {
            var newTicketId = Guid.NewGuid();

            var ticket = new Ticket
            {
                Id = newTicketId,
                Title = postRequest.Title,
                Description = postRequest.Description,
                Priority = postRequest.Priority,
                Status = 0,
                CreatedAt = DateTime.Now,
                AssigneeId = postRequest.AssigneeId,
                SubmitterId = HttpContext.GetUserId(),
                ProjectId = postRequest.ProjectId,
            };

            await _ticketService.CreateAsync(ticket);

            var newTicket = await _ticketService.GetByIdAsync(ticket.Id);
            var ticketDto = _ticketToDtoConverter.Convert(newTicket);
            var locationUri = _uriService.GetTicketUri(ticket.Id.ToString());

            return Created(locationUri, ticketDto);
        }

        [HttpPut("api/tickets/{ticketId}")]
        [Authorize(Roles = "Admin, Manager, Developer")]
        public async Task<IActionResult> Update([FromRoute]Guid ticketId, [FromBody] UpdateTicketRequest request)
        {
            var newAssignee = await _userService.GetUserByUserIdAsync(request.AssigneeId);
            var ticket = await _ticketService.GetByIdAsync(ticketId);

            ticket.Title = request.Title;
            ticket.Description = request.Description;
            ticket.Priority = request.Priority;
            ticket.Status = request.Status;
            ticket.UpdatedAt = DateTime.UtcNow;
            ticket.Assignee = newAssignee;
            ticket.AssigneeId = request.AssigneeId;

            var updated = await _ticketService.UpdateAsync(ticket);

            var ticketDto = _ticketToDtoConverter.Convert(ticket);

            if (updated)
                return Ok(ticketDto);

            return NotFound();
        }

        [HttpDelete("api/tickets/{ticketId}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Delete([FromRoute] Guid ticketId)
        {
            var deleted = await _ticketService.DeleteAsync(ticketId);

            if (deleted)
                return NoContent();

            return NotFound();
        }
    }
}