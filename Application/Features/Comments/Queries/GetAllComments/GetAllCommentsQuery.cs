using Application.Common.Models;
using Application.DTOs;
using MediatR;

namespace Application.Features.Tickets.Queries.GetAllTickets
{
    /// <summary>
    /// Récupération des commentaires avec pagination et filtres.
    /// </summary>
    public record GetAllCommentsQuery(
        int PageNumber = 1,
        int PageSize = 10,
        DateTime? CreatedAt = null,
        Guid? CreatedById = null,
        Guid? TicketId = null

    ) : IRequest<PagedResult<CommentDto>>;
}