﻿using Bugtracker.Contracts.Responses;
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
                Assignee = ticket.Assignee.UserName,
                Submitter = ticket.Submitter.UserName,
                Project = ticket.Project.Name
            };

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