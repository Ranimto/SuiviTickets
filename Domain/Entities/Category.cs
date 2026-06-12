using Domain.Communs;

namespace Domain.Entities
{
    /// <summary>
    /// Représente une catégorie de ticket.
    /// </summary>
    public class Category : AuditableEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        // Navigation
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}