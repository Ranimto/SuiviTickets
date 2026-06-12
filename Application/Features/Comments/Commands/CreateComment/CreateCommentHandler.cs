using Application.DTOs;
using Application.Interfaces.IRepository;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;

namespace Application.Features.Comments.Commands.CreateComment
{
    /// <summary>
    /// Handler de création d'un commentaire.
    /// </summary>
    public class CreateCommentHandler : IRequestHandler<CreateCommentCommand, CommentDto>
    {
        private readonly ICommentRepository _commentRepository;

        public CreateCommentHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<CommentDto> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            Comment comment = new Comment
            {
                TicketId = request.TicketId,
                Message = request.Message,
                CreatedAt = DateTime.UtcNow,
            };

            var result = await _commentRepository.AddAsync(comment, cancellationToken);
            return result.Adapt<CommentDto>();

        }
    }
}