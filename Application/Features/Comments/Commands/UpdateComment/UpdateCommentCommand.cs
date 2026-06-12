using Application.DTOs;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comments.Commands.UpdateComment
{
    /// <summary>
    /// CreateTicketCommand
    /// </summary>
    /// <param name="Title"></param>
    /// <param name="Description"></param>
    /// <param name="CategoryId"></param>
    /// <param name="CreatedById"></param>
    public record UpdateCommentCommand
    (   Guid Id,
        Guid TicketId,
        string Message
    )
     : IRequest<CommentDto>;
}
