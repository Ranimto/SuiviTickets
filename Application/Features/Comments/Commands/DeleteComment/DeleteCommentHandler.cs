using Application.Features.Comments.Commands.DeleteComment;
using Application.Interfaces.IRepository;
using Domain.Entities;
using MediatR;

namespace Application.Features.Tickets.Commands.DeleteTicket
{
    /// <summary>
    /// Handler de suppression d'un ticket.
    /// </summary>
    public class DeleteCommentHandler : IRequestHandler<DeleteCommentCommand, Guid>
    {
        private readonly ICommentRepository _commentRepository;

        public DeleteCommentHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<Guid> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            await _commentRepository.DeleteByIdAsync(request.Id, cancellationToken);
            return request.Id;
        }
    }
}