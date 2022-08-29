using AutoMapper;
using Vulns.Core;
using Newtonsoft.Json.Linq;

namespace Vulns.Jobs.Issuers;

internal class IssuersMapping : Profile
{
    public IssuersMapping()
    {
        CreateMap<JObject, Issuer>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s["cnaID"]))
            .ForMember(d => d.Organization, o => o.MapFrom(s => s["organizationName"]))
            .ForMember(d => d.ShortName, o => o.MapFrom(s => s["shortName"]))
            .ForMember(d => d.Scope, o => o.MapFrom(s => s["scope"]))
            .ForMember(d => d.Country, o => o.MapFrom(s => s["country"]))
            .ForMember(d => d.Email, o => o.ConvertUsing(new MitreJsonEmailConverter(), s => s));
    }

    internal class MitreJsonEmailConverter : IValueConverter<JObject, string?>
    {
        public string? Convert(JObject source, ResolutionContext context)
        {
            JArray? contactsArray = (JArray?)source["contact"];
            if (contactsArray == null || contactsArray.Count == 0) return null;

            JObject contactElement = (JObject)contactsArray.First();
            JArray? emailsArray = (JArray?)contactElement["email"];
            if (emailsArray == null || emailsArray.Count == 0) return null;

            JObject emailElement = (JObject)emailsArray.First();
            var emailAddress = emailElement["emailAddr"];
            return emailAddress == null ? null : emailAddress.ToString();
        }
    }
}