using Domain.Communs;
using Domain.Enums;

namespace Domain.Entities
{
    /// <summary>
    /// Historique des changements de statut d’un ticket.
    /// </summary>
    public class TicketHistory : AuditableEntity
    {
        public Guid TicketId { get; set; }

        /// <summary>
        /// Ancien statut.
        /// </summary>
        public TicketStatus OldStatus { get; set; }

        /// <summary>
        /// Nouveau statut.
        /// </summary>
        public TicketStatus NewStatus { get; set; }

        /// <summary>
        /// Représente qui a changé le statut
        /// </summary>
        public Guid ChangedById { get; set; }


        // Navigation
        public Ticket Ticket { get; set; } = null!;
    }
}