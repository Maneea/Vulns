using AutoMapper;

using Vulns.App;
using Vulns.Core;
namespace Vulns.Web;

internal class ProductMappings : Profile
{
    public ProductMappings()
    {
        CreateMap<ProductUri, ProductFragment>()
            .ForMember(d => d.Type, o => o.MapFrom(s => s.ProductType.ToString()))
            .ForMember(d => d.Vendor, o => o.MapFrom(s => s.FormattedVendor))
            .ForMember(d => d.Product, o => o.MapFrom(s => s.FormattedProduct))
            .ForMember(d => d.Version, o => o.MapFrom(s => s.FormattedVersion));
        CreateMap<Product, ProductFragment>()
            .AfterMap((src, dest, ctx) => ctx.Mapper.Map(src.Uri, dest));

        CreateMap<ProductUri, ProductDetails>()
            .ForMember(d => d.Type, o => o.MapFrom(s => s.ProductType.ToString()))
            .ForMember(d => d.Vendor, o => o.MapFrom(s => s.FormattedVendor))
            .ForMember(d => d.Product, o => o.MapFrom(s => s.FormattedProduct))
            .ForMember(d => d.Version, o => o.MapFrom(s => s.FormattedVersion))
            .ForMember(d => d.Update, o => o.MapFrom(s => s.FormattedUpdate))
            .ForMember(d => d.Edition, o => o.MapFrom(s => s.FormattedEdition))
            .ForMember(d => d.SoftwareEdition, o => o.MapFrom(s => s.FormattedSoftwareEdition))
            .ForMember(d => d.TargetSoftware, o => o.MapFrom(s => s.FormattedTargetSoftware))
            .ForMember(d => d.TargetHardware, o => o.MapFrom(s => s.FormattedTargetHardware))
            .ForMember(d => d.Language, o => o.MapFrom(s => s.FormattedLanguage))
            .ForMember(d => d.Other, o => o.MapFrom(s => s.FormattedOther));
        CreateMap<Product, ProductDetails>()
            .AfterMap((src, dest, ctx) => ctx.Mapper.Map(src.Uri, dest));

        CreateMap<Vulns.Core.ProductReference, ProductReference>();
        CreateMap<ProductSearchFilters, ProductSearchCriteria>();
    }
}