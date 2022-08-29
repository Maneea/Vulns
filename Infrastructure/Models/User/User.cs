using Microsoft.AspNetCore.Identity;

namespace Vulns.Infrastructure;
// Although this should be in the `Core` project, ASP.NET Core Identity has 
// a structure that makes this difficult. They require the custom `User` class
// to inherit from `IdentityUser`, which the `Core` project does not have access
// to. Hence, we put this class in this project, as it is the only project (aside
// from `Api`) that includes the Identity NuGet package and can access/inherit from
// `IdentityUser`.

public class User : IdentityUser
{
    public string? DashboardWidgets { get; set; } = string.Empty;
    public User() : base() { }
    public User(string? username) : base(username) { }
}