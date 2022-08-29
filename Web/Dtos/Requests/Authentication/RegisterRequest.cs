using System.ComponentModel.DataAnnotations;

namespace Vulns.Web;

public class RegisterRequest
{
    [Required]
    public string? Username { get; set; }

    [Required]
    public string? Password { get; set; }

    [Required]
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}