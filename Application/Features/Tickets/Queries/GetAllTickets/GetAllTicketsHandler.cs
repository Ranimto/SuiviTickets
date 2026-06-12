using Application.Common.Models;
using Application.DTOs;
using Application.Interfaces.IRepository;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Features.Tickets.Queries.GetAllTickets
{
    /// <summary>
    /// Handler de récupération des tickets avec filtres.
    /// </summary>
    public class GetAllTicketsHandler: IRequestHandler<GetAllTicketsQuery, PagedResult<TicketDto>>
    {
        private readonly ITicketRepository _ticketRepository;

        public GetAllTicketsHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<PagedResult<TicketDto>> Handle(GetAllTicketsQuery request,CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            // récupération base
            var query = await _ticketRepository.GetAllAsync(cancellationToken);

            // FILTERING
            if (!string.IsNullOrEmpty(request.Status))
            {
                query = query.Where(t => t.Status.ToString() == request.Status).ToList();
            }

            if (!string.IsNullOrEmpty(request.Priority))
            {
                query = query.Where(t => t.Priority.ToString() == request.Priority).ToList();
            }

            if (request.CategoryId.HasValue)
            {
                query = query.Where(t => t.CategoryId == request.CategoryId).ToList();
            }


            var totalCount = query.Count();

            // PAGINATION
            var items = query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            // MAP vers DTO
            var result = items.Adapt<List<TicketDto>>();

            return new PagedResult<TicketDto>
            {
                Items = result,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = totalCount
            };
        }
    }
}