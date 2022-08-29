using AutoMapper;

using Microsoft.AspNetCore.Identity;
namespace Vulns.Web;

internal class AuthenticationMappings : Profile
{
    public AuthenticationMappings()
    {
        CreateMap<IdentityError, AuthenticationError>();
    }
}