using Application.DTOs;
using MediatR;

namespace Application.Features.Comments.Commands.CreateComment
{
    /// <summary>
    /// CreateCommentCommand
    /// </summary>
    /// <param name="TicketId"></param>
    /// <param name="Message"></param>
    public record CreateCommentCommand
        (
        Guid TicketId,
        string Message
        ) : IRequest<CommentDto>;
}
