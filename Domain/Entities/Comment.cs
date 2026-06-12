using Domain.Communs;

namespace Domain.Entities
{
    /// <summary>
    /// Représente un commentaire sur un ticket.
    /// </summary>
    public class Comment : AuditableEntity
    {
        public Guid TicketId { get; set; }

        /// <summary>
        /// Contenu du commentaire.
        /// </summary>
        public string Message { get; set; } = null!;

        // Navigation
        public Ticket Ticket { get; set; } = null!;
    }
}