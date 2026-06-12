namespace Application.Common
{
    public class JwtSettings
    {
        // To sign token 
        public string SecretKey { get; set; } = null!;

        public string Issuer { get; set; } = null!;

        public string Audience { get; set; } = null!;

        public int ExpirationMinutes { get; set; }
        public double RefreshTokenExpirationDays { get; set; }
    }
}
