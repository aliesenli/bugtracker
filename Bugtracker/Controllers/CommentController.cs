using System;
using System.Threading.Tasks;
using Bugtracker.Domain;
using Bugtracker.Extensions;
using Bugtracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bugtracker.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("api/tickets/{ticketId}/comments")]
        public async Task<IActionResult> GetAllCommentsOfTicket([FromRoute] Guid ticketId)
        {
            var comments = await _commentService.GetAllAsync(ticketId);
            //var commentsDto = _commentToDtoListConverter.Convert(comments);

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

            var created = await _commentService.CreateAsync(comment);
            if (created)
                return Ok();

            return NotFound();
        }
    }

}