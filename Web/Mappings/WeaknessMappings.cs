using AutoMapper;

using Vulns.Core;
namespace Vulns.Web;

internal class WeaknessMappings : Profile
{
    public WeaknessMappings()
    {
        CreateMap<Weakness, WeaknessFragment>();
        CreateMap<Weakness, WeaknessDetails>();
        CreateMap<Vulns.Core.WeaknessConsequence, WeaknessConsequence>();
        CreateMap<Vulns.Core.WeaknessDetection, WeaknessDetection>();
        CreateMap<Vulns.Core.WeaknessPlatform, WeaknessPlatform>();
    }
}