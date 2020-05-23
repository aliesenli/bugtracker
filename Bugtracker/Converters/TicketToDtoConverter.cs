using Bugtracker.Contracts.Responses;
using Bugtracker.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Bugtracker.Converters
{
    public class TicketToDtoConverter : IConverter<Ticket, TicketResponse>, IConverter<IList<Ticket>, IList<TicketResponse>>
    {
        public TicketResponse Convert(Ticket ticket)
        {
            var ticketDto = new TicketResponse
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                Priority = ticket.Priority.ToString(),
                Status = ticket.Status.ToString(),
                CreatedOn = ticket.CreatedAt.ToString(),
                UpdatedOn = ticket.UpdatedAt.ToString(),
                Submitter = ticket.Submitter.UserName,
                Assignee = ticket.Assignee != null ? ticket.Assignee.UserName : "",
                AssigneeId = ticket.AssigneeId != null ? ticket.Assignee.Id : "",
                Project = ticket.Project.Name
            };

            if (ticket.Audits != null)
            {
                foreach (var audit in ticket.Audits)
                {
                    var auditDto = new AuditResponse
                    {
                        Id = audit.Id,
                        Property = audit.Property,
                        NewValue = audit.NewValue,
                        OldValue = audit.OldValue,
                        Date = audit.Date.ToString()
                    };

                    ticketDto.Audits.Add(auditDto);
                }
            }

            if (ticket.Comments != null)
            {
                foreach (var comment in ticket.Comments)
                {
                    var commentDto = new CommentResponse
                    {
                        Id = comment.Id,
                        Message = comment.Message,
                        Writer = comment.Writer.UserName
                    };

                    ticketDto.Comments.Add(commentDto);
                }
            }

            return ticketDto;
        }

        public IList<TicketResponse> Convert(IList<Ticket> tickets)
        {
            return tickets.Select(tck =>
            {
                var ticketDto = Convert(tck);
                return ticketDto;
            }).ToList();
        }
    }
}
