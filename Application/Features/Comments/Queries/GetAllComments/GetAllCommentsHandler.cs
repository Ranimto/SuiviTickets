using Application.Common.Models;
using Application.DTOs;
using Application.Interfaces.IRepository;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Features.Tickets.Queries.GetAllTickets
{
    /// <summary>
    /// Handler de récupération des commentaires avec filtres.
    /// </summary>
    public class GetAllCommentsHandler: IRequestHandler<GetAllCommentsQuery, PagedResult<CommentDto>>
    {
        private readonly ICommentRepository _commentRepository;

        public GetAllCommentsHandler(ICommentRepository commentRepository) 
        {
         _commentRepository = commentRepository;
        }

        public async Task<PagedResult<CommentDto>> Handle(GetAllCommentsQuery request,CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            // récupération base
            var query = await _commentRepository.GetAllAsync(cancellationToken);

            // FILTERING
            if (!string.IsNullOrEmpty(request.CreatedAt.ToString()))
            {
                query = query.Where(t => t.CreatedAt == request.CreatedAt).ToList();
            }

            if (request.CreatedById.HasValue)
            {
                query = query.Where(t => t.CreatedById == request.CreatedById).ToList();
            }

            if (request.TicketId.HasValue)
            {
                query = query.Where(t => t.TicketId == request.TicketId).ToList();
            }


            var totalCount = query.Count();

            // PAGINATION
            var items = query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            // MAP vers DTO
            var result = items.Adapt<List<CommentDto>>();

            return new PagedResult<CommentDto>
            {
                Items = result,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = totalCount
            };
        }
    }
}