using AutoMapper;

using Vulns.Core;
namespace Vulns.Web;

internal class IssuerMappings : Profile
{
    public IssuerMappings()
    {
        CreateMap<Issuer, IssuerFragment>()
            .ForMember(d => d.ShortName, o => o.MapFrom(s => s.ShortName.Humanize(true)));
        CreateMap<Issuer, IssuerDetails>()
            .ForMember(d => d.ShortName, o => o.MapFrom(s => s.ShortName.Humanize(true)));
    }
}