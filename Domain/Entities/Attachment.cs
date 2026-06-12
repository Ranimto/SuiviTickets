using Domain.Communs;

namespace Domain.Entities
{
    /// <summary>
    /// Représente un fichier attaché à un ticket.
    /// </summary>
    public class Attachment : AuditableEntity
    {
        public Guid TicketId { get; set; }

        /// <summary>
        /// URL du fichier.
        /// </summary>
        public string FileUrl { get; set; } = null!;

        /// <summary>
        /// Type du fichier.
        /// </summary>
        public string FileType { get; set; } = null!;

        // Navigation
        public Ticket Ticket { get; set; } = null!;
    }
}