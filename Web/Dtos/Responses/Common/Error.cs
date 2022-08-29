namespace Vulns.Web;
public class Error : IResponse
{
    public string ErrorMessage { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public Error(string message) => ErrorMessage = message;
}
