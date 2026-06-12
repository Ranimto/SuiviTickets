using Application.DTOs;
using Application.Features.Comments.Queries.GetTicketById;
using Application.Features.Tickets.Commands;
using Application.Interfaces.IRepository;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Features.Tickets.Queries.GetTicketById
{
    /// <summary>
    /// Handler de lecture d'un ticket.
    /// </summary>
    public class GetTicketByIdHandler : IRequestHandler<GetTicketByIdQuery, TicketDto>
    {
        private readonly IRepository<Ticket> _ticketRepository;

        public GetTicketByIdHandler(IRepository<Ticket> ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<TicketDto> Handle(GetTicketByIdQuery request,CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var ticket = await _ticketRepository
                .GetByIdAsync(request.Id, cancellationToken);

            if (ticket is null)
                throw new Exception("Ticket not found");

            return ticket.Adapt<TicketDto>();
        }
    }
}