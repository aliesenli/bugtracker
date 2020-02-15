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

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
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
    }

}