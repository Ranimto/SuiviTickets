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
    public class GetCommentsByTicketIdHandler : IRequestHandler<GetCommentsByTicketIdQuery, List<CommentDto>>
    {
        private readonly ICommentRepository _commentRepository;

        public GetCommentsByTicketIdHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<List<CommentDto>> Handle(GetCommentsByTicketIdQuery request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            if (string.IsNullOrEmpty(request.TicketId.ToString())) 
            {
                throw new Exception("Ticket Id is null");
            }

             var comments = _commentRepository.GetCommentsByTicketIdAsync(request.TicketId);
             return comments.Adapt<List<CommentDto>>();
        }

    }
}