using Application.DTOs;
using Application.Interfaces.IRepository;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;

namespace Application.Features.Tickets.Commands.CreateTicket
{
    /// <summary>
    /// Handler de création d'un ticket.
    /// </summary>
    public class CreateTicketCommandHandler: IRequestHandler<CreateTicketCommand, TicketDto>
    {
        private readonly ITicketRepository _ticketRepository;

        public CreateTicketCommandHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<TicketDto> Handle( CreateTicketCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

  
            var ticket = new Ticket
            {
                Title = request.Title,
                Description = request.Description,

                CategoryId = request.CategoryId,

                CreatedById = request.CreatedById,

                CreatedAt = DateTime.UtcNow,

                Status = TicketStatus.Open,

                Priority = TicketPriority.Medium
            };

            var result = await _ticketRepository
                .AddAsync(ticket, cancellationToken);


            return result.Adapt<TicketDto>();
        }
    }
}