using System.ComponentModel.DataAnnotations;

namespace Domain.Communs
{
    /// <summary>
    /// Classe de base pour toutes les entités du domaine.
    /// Contient les propriétés communes.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Identifiant unique de l'entité.
        /// </summary>
        [Key]
        public Guid Id { get; set; }
    }
}