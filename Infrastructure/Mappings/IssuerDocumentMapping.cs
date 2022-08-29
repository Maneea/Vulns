using AutoMapper;

using Vulns.Core;
namespace Vulns.Infrastructure;

internal class IssuerDocumentMapping : Profile
{
    public IssuerDocumentMapping()
    {
        CreateMap<IssuerDocument, Issuer>()
            .ReverseMap();
    }
}