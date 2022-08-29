using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

using Vulns.Infrastructure;

namespace Vulns.Services;

public class JwtService
{
    private readonly IConfiguration _conf;
    private TimeSpan _tokenValidity = TimeSpan.FromDays(1);
    public JwtService(IConfiguration conf)
    {
        _conf = conf;
    }

    public JwtSecurityToken GetToken(IList<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_conf["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer: _conf["JWT:ValidIssuer"],
            audience: _conf["JWT:ValidAudience"],
            expires: DateTime.Now.Add(_tokenValidity),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return token;
    }

    internal IList<Claim> GenerateUserClaims(User user, IList<string> roles)
    {
        var claims = new List<Claim>();
    
        if (user.Id != null)
            claims.Add(new(ClaimTypes.NameIdentifier, user.Id));

        if (user.UserName != null)
            claims.Add(new(ClaimTypes.Name, user.UserName));

        if (user.Email != null)
            claims.Add(new(ClaimTypes.Email, user.Email));

        if (user.PhoneNumber != null)
            claims.Add(new(ClaimTypes.MobilePhone, user.PhoneNumber));

        foreach (var userRole in roles)
            claims.Add(new(ClaimTypes.Role, userRole));
            
        return claims;
    }
}