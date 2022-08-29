using AutoMapper;

using Vulns.Core;

namespace Vulns.Jobs.Products;
public class ProductsMapping : Profile
{
    public ProductsMapping()
    {
        CreateMap<NvdCpeItem, Product>()
            .ConstructUsing(item => new Product(item.AnySpecified ? item.Any.First().GetAttribute("name") : string.Empty))
            .ForMember(d => d.Title, o => o.MapFrom(s => s.TitleSpecified ?
                s.Title.Where(
                    t => t.Lang == "en-US"
                ).First().Value.TitleCase() :
                string.Empty
            ))
            .ForMember(d => d.References, o => o.ConvertUsing(new ReferencesToReferenceListConverter(), s => s))
            .AfterMap((s, d, ctx) => d.Uri.ApplyFormatting(d.Title ?? string.Empty));

        CreateMap<NvdCpeUpdateFeedsRoot.Cpe, Product>()
            .ConstructUsing(cpe => new(cpe.Cpe23Uri))
            .ForMember(d => d.Title, o => o.MapFrom(
                s => s.Titles.Where(title => title.IsEnglish()).Any() ? s.Titles.Where(title => title.IsEnglish()).First().Name.TitleCase() : string.Empty))
            .ForMember(d => d.ModifiedAt, o => o.MapFrom(s => s.LastModifiedDate))
            .ForMember(d => d.References, o => o.ConvertUsing(new NvdCpeUpdatesReferenceListConverter(), s => s))
            .AfterMap((s, d, ctx) => d.Uri.ApplyFormatting(d.Title ?? string.Empty));
    }
}

internal class NvdCpeUpdatesReferenceListConverter : IValueConverter<NvdCpeUpdateFeedsRoot.Cpe, List<ProductReference>>
{
    public List<ProductReference> Convert(NvdCpeUpdateFeedsRoot.Cpe source, ResolutionContext context)
    {
        var references = source.Refs;
        var id = source.Cpe23Uri;
        List<ProductReference> refs = new();
        var advisory = nameof(ProductReferenceTag.Advisory).ToLower(); // or advisories or security
        var changeLog = nameof(ProductReferenceTag.ChangeLog).ToLower(); // or (change log) with space, or history
        var version = nameof(ProductReferenceTag.Version).ToLower(); // or release
        var product = nameof(ProductReferenceTag.Product).ToLower(); // or software
        var project = nameof(ProductReferenceTag.Project).ToLower();
        var vendor = nameof(ProductReferenceTag.Vendor).ToLower();
        foreach (var reference in references)
        {
            // Check from the most specific in ProductReferenceTag to the least specific, and assign the appropriate tag
            var tag = reference.Type.ToLower();
            if (tag.Contains(advisory) || tag.Contains("advisories") || tag.Contains("security"))
                refs.Add(new(id, ProductReferenceTag.Advisory, reference.Reference));
            else if (tag.Contains(changeLog) || tag.Contains("change log") || tag.Contains("history"))
                refs.Add(new(id, ProductReferenceTag.ChangeLog, reference.Reference));
            else if (tag.Contains(version) || tag.Contains("release"))
                refs.Add(new(id, ProductReferenceTag.Version, reference.Reference));
            else if (tag.Contains(product) || tag.Contains("software"))
                refs.Add(new(id, ProductReferenceTag.Product, reference.Reference));
            else if (tag.Contains(project))
                refs.Add(new(id, ProductReferenceTag.Project, reference.Reference));
            else if (tag.Contains(vendor))
                refs.Add(new(id, ProductReferenceTag.Vendor, reference.Reference));
            else
                refs.Add(new(id, ProductReferenceTag.Unspecified, reference.Reference));
        }
        refs.ForEach(r => r.Id = $"{r.Id}-{r.Tag.Value}-{r.Url.GetHashCode()}");
        refs = refs.DistinctBy(r => r.Id).ToList();
        return refs;
    }
}

internal class ReferencesToReferenceListConverter : IValueConverter<NvdCpeItem, List<ProductReference>>
{
    public List<ProductReference> Convert(NvdCpeItem source, ResolutionContext context)
    {
        var references = source.References;
        var id = source.AnySpecified ? source.Any.First().GetAttribute("name") : string.Empty;
        List<ProductReference> refs = new();
        var advisory = nameof(ProductReferenceTag.Advisory).ToLower(); // or advisories or security
        var changeLog = nameof(ProductReferenceTag.ChangeLog).ToLower(); // or (change log) with space, or history
        var version = nameof(ProductReferenceTag.Version).ToLower(); // or release
        var product = nameof(ProductReferenceTag.Product).ToLower(); // or software
        var project = nameof(ProductReferenceTag.Project).ToLower();
        var vendor = nameof(ProductReferenceTag.Vendor).ToLower();
        foreach (var reference in references)
        {
            // Check from the most specific in ProductReferenceTag to the least specific, and assign the appropriate tag
            var tag = reference.Value.ToLower();
            if (tag.Contains(advisory) || tag.Contains("advisories") || tag.Contains("security"))
                refs.Add(new(id, ProductReferenceTag.Advisory, reference.Href));
            else if (tag.Contains(changeLog) || tag.Contains("change log") || tag.Contains("history"))
                refs.Add(new(id, ProductReferenceTag.ChangeLog, reference.Href));
            else if (tag.Contains(version) || tag.Contains("release"))
                refs.Add(new(id, ProductReferenceTag.Version, reference.Href));
            else if (tag.Contains(product) || tag.Contains("software"))
                refs.Add(new(id, ProductReferenceTag.Product, reference.Href));
            else if (tag.Contains(project))
                refs.Add(new(id, ProductReferenceTag.Project, reference.Href));
            else if (tag.Contains(vendor))
                refs.Add(new(id, ProductReferenceTag.Vendor, reference.Href));
            else
                refs.Add(new(id, ProductReferenceTag.Unspecified, reference.Href));
        }
        refs.ForEach(r => r.Id = $"{r.Id}-{r.Tag.Value}-{r.Url.GetHashCode()}");
        refs = refs.DistinctBy(r => r.Id).ToList();
        return refs;
    }
}
