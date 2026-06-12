public class RefreshTokenResult
{
    public string Token { get; set; } = default!;
    public string HashedToken { get; set; } = default!;
    public DateTime Expires { get; set; }
}
