using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bugtracker.Domain;
using Bugtracker.Services;
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

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("api/tickets")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllTicketsQuery getAllTicketsQuery)
        {
            var tickets = await _ticketService.GetTicketsAsync();

            return Ok(tickets);
        }
    }

    public class GetAllTicketsQuery
    {
        [FromQuery(Name = "userId")]
        public string UserId { get; set; }
    }
}