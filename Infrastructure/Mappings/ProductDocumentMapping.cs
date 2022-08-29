using AutoMapper;

using Vulns.Core;
namespace Vulns.Infrastructure;

internal class ProductDocumentMapping : Profile
{
    public ProductDocumentMapping()
    {
        CreateMap<ProductDocument, Product>()
            .ConstructUsing(s => new Product(s.Id!))
            .AfterMap((src, dest, context) => dest.Uri.ApplyFormatting(dest.Title ?? string.Empty))
            .ReverseMap()
            .AfterMap((src, dest, context) => context.Mapper.Map(src.Uri, dest));

        CreateMap<ProductUri, ProductDocument>()
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
    }
}