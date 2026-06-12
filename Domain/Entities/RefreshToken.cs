
namespace Domain.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }

        public string TokenHash { get; set; } = null!;

        public DateTime Expires { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Revoked { get; set; }

        public string AppUserId { get; set; } 

        // Business logic
        public bool IsExpired => DateTime.UtcNow >= Expires;

        public bool IsActive => Revoked == null && !IsExpired;

        public bool IsRevoked { get; set; }
    }
}
