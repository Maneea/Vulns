namespace Vulns.Web;
public class JwtToken : IResponse
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
    public DateTime Timestamp {get; set;} = DateTime.UtcNow;
    public JwtToken(string token, DateTime expiration) => (Token, Expiration) = (token, expiration);
}