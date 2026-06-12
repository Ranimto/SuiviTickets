using Application.DTOs;
using Application.Interfaces.IRepository;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;

namespace Application.Features.Tickets.Commands.UpdateTicket
{
    /// <summary>
    /// Handler de mise à jour d'un ticket.
    /// </summary>
    public class UpdateTicketCommandHandler: IRequestHandler<UpdateTicketCommand, TicketDto>
    {
        private readonly IRepository<Ticket> _ticketRepo;

        public UpdateTicketCommandHandler(IRepository<Ticket> ticketRepo)
        {
            _ticketRepo = ticketRepo;
        }

        public async Task<TicketDto> Handle(
            UpdateTicketCommand request,
            CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var ticket = await _ticketRepo.GetByIdAsync(
                request.Id,
                cancellationToken);

            if (ticket is null)
                throw new Exception("Ticket not found");

            ticket.Title = request.Title;
            ticket.Description = request.Description;
            ticket.Priority = request.Priority;
            ticket.Status = request.Status;
            ticket.CategoryId = request.CategoryId;

            var updatedTicket = await _ticketRepo.UpdateAsync(
                ticket,
                cancellationToken);

            return updatedTicket.Adapt<TicketDto>();
        }
    }
}