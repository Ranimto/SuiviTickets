using Application.Common.Models;
using Application.DTOs;
using MediatR;

namespace Application.Features.Tickets.Queries.GetAllTickets
{
    /// <summary>
    /// Récupération des tickets avec pagination et filtres.
    /// </summary>
    public record GetAllTicketsQuery(
        int PageNumber = 1,
        int PageSize = 10,
        string? Status = null,
        string? Priority = null,
        Guid? CategoryId = null
    ) : IRequest<PagedResult<TicketDto>>;
}