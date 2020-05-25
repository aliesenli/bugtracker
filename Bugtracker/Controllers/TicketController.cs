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
using Swashbuckle.AspNetCore.Annotations;

namespace Bugtracker.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [SwaggerTag("Create, Read, Update and Delete Tickets")]
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
        [SwaggerOperation("Returns All Tickets")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var tickets = await _ticketService.GetAllAsync();
            var ticketsDto = _ticketToDtoListConverter.Convert(tickets);

            return Ok(ticketsDto);
        }

        [HttpGet("api/tickets/{ticketId}")]
        [SwaggerOperation("Returns a Single Ticket")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute]Guid ticketId)
        {
            var ticket = await _ticketService.GetByIdAsync(ticketId);
            var ticketDto = _ticketToDtoConverter.Convert(ticket);

            return Ok(ticketDto);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost("api/tickets/create")]
        [SwaggerOperation("Create a new Ticket")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
                SubmitterId = HttpContext.GetUserId(),
                AssigneeId = postRequest.AssigneeId != "" ? postRequest.AssigneeId : null,
                ProjectId = postRequest.ProjectId,
            };

            await _ticketService.CreateAsync(ticket);

            var newTicket = await _ticketService.GetByIdAsync(ticket.Id);
            var ticketDto = _ticketToDtoConverter.Convert(newTicket);
            var locationUri = _uriService.GetTicketUri(ticket.Id.ToString());

            return Created(locationUri, ticketDto);
        }

        [Authorize(Roles = "Admin, Manager, Developer")]
        [HttpPut("api/tickets/{ticketId}")]
        [SwaggerOperation("Update a Ticket")]
        public async Task<IActionResult> Update([FromRoute]Guid ticketId, [FromBody] UpdateTicketRequest request)
        {
            var newAssignee = await _userService.GetUserByUserIdAsync(request.AssigneeId);
            var ticket = await _ticketService.GetByIdAsync(ticketId);

            ticket.Title = request.Title;
            ticket.Description = request.Description;
            ticket.Priority = request.Priority;
            ticket.Status = request.Status;
            ticket.UpdatedAt = DateTime.UtcNow;
            ticket.AssigneeId = newAssignee?.Id;

            var updated = await _ticketService.UpdateAsync(ticket);

            var ticketDto = _ticketToDtoConverter.Convert(ticket);

            if (updated)
                return Ok(ticketDto);

            return NotFound();
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpDelete("api/tickets/{ticketId}")]
        [SwaggerOperation("Delete a Ticket")]
        public async Task<IActionResult> Delete([FromRoute] Guid ticketId)
        {
            var deleted = await _ticketService.DeleteAsync(ticketId);

            if (deleted)
                return NoContent();

            return NotFound();
        }

        [HttpGet("api/tickets/{ticketId}/comments")]
        public async Task<IActionResult> GetAllCommentsOfTicket([FromRoute] Guid ticketId)
        {
            var comments = await _ticketService.GetAllCommentsAsync(ticketId);

            return Ok(comments);
        }

        [HttpPost("api/tickets/{ticketId}/comments/create")]
        public async Task<IActionResult> Create([FromRoute] Guid ticketId, [FromBody] CreateCommentRequest request)
        {
            var newCommentId = Guid.NewGuid();

            var comment = new Comment
            {
                Id = newCommentId,
                Message = request.Message,
                TicketId = ticketId,
                WriterId = HttpContext.GetUserId()
            };

            var created = await _ticketService.CreateCommentAsync(comment);
            if (created)
            {
                var newComment = await _ticketService.GetCommentByIdAsync(comment.Id);
                return Ok(newComment);
            }

            return NotFound();
        }
    }
}