using Application.DTOs;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comments.Queries.GetCommentsById
{
    /// <summary>
    /// GetCommentByTicketIdQuery
    /// </summary>
    public record GetCommentsByTicketIdQuery(Guid TicketId): IRequest<CommentDto>;
}
