namespace Domain.Communs
{
    /// <summary>
    /// Entité avec audit (création et modification).
    /// </summary>
    public abstract class AuditableEntity : BaseEntity
    {
        /// <summary>
        /// Date de création de l'entité.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Date de dernière modification.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Identifiant de l'utilisateur ayant créé l'entité.
        /// </summary>
        public Guid CreatedById { get; set; }

        /// <summary>
        /// Identifiant de l'utilisateur ayant modifié l'entité.
        /// </summary>
        public Guid? UpdatedById { get; set; }
    }
}