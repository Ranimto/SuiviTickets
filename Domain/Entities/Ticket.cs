using Domain.Communs;
using Domain.Enums;
using Domain.Events;

namespace Domain.Entities
{
    /// <summary>
    /// Représente un ticket IT.
    /// </summary>
    public class Ticket : AuditableEntity
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public TicketPriority Priority { get; set; }
        public TicketStatus Status { get; set; }

        public Guid? AssignedToId { get; set; }
        public Guid CategoryId { get; set; }

        public Category Category { get; set; } = null!;
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
        public ICollection<TicketHistory> Histories { get; set; } = new List<TicketHistory>();

        /// <summary>
        /// Factory method pour créer un ticket.
        /// </summary>
        public static Ticket Create(string title, string description, Guid userId, Guid categoryId)
        {
            var ticket = new Ticket
            {
                Id = Guid.NewGuid(),
                Title = title,
                Description = description,
                Priority = TicketPriority.Medium,
                Status = TicketStatus.Open,
                CreatedAt = DateTime.UtcNow,
                CreatedById = userId,
                CategoryId = categoryId
            };

            ticket.AddDomainEvent(new TicketCreatedEvent(ticket.Id));

            return ticket;
        }

        private void AddDomainEvent(TicketCreatedEvent ticketCreatedEvent)
        {
            throw new NotImplementedException();
        }
    }
}