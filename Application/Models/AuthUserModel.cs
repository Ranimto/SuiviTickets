using Domain.Entities;

namespace Application.Models
{
    /// <summary>
    /// Class for authentification and  JWT generate
    /// </summary>
    public class AuthUserModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public IList<string> Roles { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; } = new();
    }
}
