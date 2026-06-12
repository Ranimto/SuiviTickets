using Application.DTOs;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tickets.Commands
{
    /// <summary>
    /// CreateTicketCommand
    /// </summary>
    /// <param name="Title"></param>
    /// <param name="Description"></param>
    /// <param name="CategoryId"></param>
    /// <param name="CreatedById"></param>
    public record GetTicketByIdQuery(Guid Id): IRequest<TicketDto>;
}
