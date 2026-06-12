using Application.Interfaces.IRepository;
using Domain.Entities;
using MediatR;

namespace Application.Features.Tickets.Commands.DeleteTicket
{
    /// <summary>
    /// Handler de suppression d'un ticket.
    /// </summary>
    public class DeleteTicketCommandHandler: IRequestHandler<DeleteTicketCommand, Guid>
    {
        private readonly IRepository<Ticket> _ticketRepository;

        public DeleteTicketCommandHandler(IRepository<Ticket> ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<Guid> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            await _ticketRepository.DeleteByIdAsync( request.Id, cancellationToken);

            return request.Id;
        }
    }
}