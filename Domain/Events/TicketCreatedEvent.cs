using Domain.Communs;

namespace Domain.Events
{
    /// <summary>
    /// Événement déclenché lors de la création d'un ticket.
    /// </summary>
    public class TicketCreatedEvent : DomainEvent
    {
        /// <summary>
        /// Identifiant du ticket créé.
        /// </summary>
        public Guid TicketId { get; }

        /// <summary>
        /// Constructeur de l'événement.
        /// </summary>
        public TicketCreatedEvent(Guid ticketId)
        {
            TicketId = ticketId;
        }
    }
}