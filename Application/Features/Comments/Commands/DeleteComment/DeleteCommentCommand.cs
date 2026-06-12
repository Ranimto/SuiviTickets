using MediatR;

namespace Application.Features.Comments.Commands.DeleteComment
{

    /// <summary>
    /// DeleteCommentCommand
    /// </summary>
    /// <param name="Id"></param>
    public record DeleteCommentCommand(Guid Id) : IRequest<Guid>;
}
