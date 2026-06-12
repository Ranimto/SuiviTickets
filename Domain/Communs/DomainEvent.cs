using MediatR;

namespace Domain.Communs
{
    /// <summary>
    /// Classe de base pour les événements de domaine.
    /// </summary>
    public abstract class DomainEvent : INotification
    {
        /// <summary>
        /// Date de déclenchement de l'événement.
        /// </summary>
        public DateTime OccurredOn { get; protected set; } = DateTime.UtcNow;
    }
}