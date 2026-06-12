using Application.DTOs;
using Application.Interfaces.IRepository;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Features.Comments.Queries.GetCommentsById
{
    /// <summary>
    /// Handler de lecture d'un ticket.
    /// </summary>
    public class GetCommentsByIdHandler : IRequestHandler<GetCommentsByIdQuery, CommentDto>
    {
        private readonly ICommentRepository _commentRepository;

        public GetCommentsByIdHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<CommentDto> Handle(GetCommentsByIdQuery request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            if (string.IsNullOrEmpty(request.Id.ToString())) 
            {
                throw new Exception("Id is null");
            }

             var comment = _commentRepository.GetByIdAsync(request.Id);
             return comment.Adapt<CommentDto>();
        }

    }
}