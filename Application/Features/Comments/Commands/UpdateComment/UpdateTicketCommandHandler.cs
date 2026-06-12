using Application.DTOs;
using Application.Interfaces.IRepository;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Features.Comments.Commands.UpdateComment
{
    /// <summary>
    /// Handler de mise à jour d'un Comment.
    /// </summary>
    public class UpdateCommentCommandHandler: IRequestHandler<UpdateCommentCommand, CommentDto>
    {
        private readonly IRepository<Comment> _commentRepo;

        public UpdateCommentCommandHandler(IRepository<Comment> commentRepo)
        {
            _commentRepo = commentRepo;
        }

        public async Task<CommentDto> Handle( UpdateCommentCommand request,CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var comment = await _commentRepo.GetByIdAsync(
                request.Id,
                cancellationToken);

            if (comment is null)
                throw new Exception("comment not found");

            comment.TicketId = request.TicketId;
            comment.Message = request.Message;
            comment.UpdatedAt = DateTime.Now;

            var updatedComment = await _commentRepo.UpdateAsync(comment,cancellationToken);

            return updatedComment.Adapt<CommentDto>();
        }
    }
}