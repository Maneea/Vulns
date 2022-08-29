using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Nest;

using Vulns.Core;

namespace Vulns.Infrastructure;
public class ProductRepository : SearchableRepository<Product, ProductDocument>
{
    public ProductRepository(IElasticClient elastic, AppDbContext context, IMapper mapper) : base(elastic, context, mapper) { }

    public async Task<IEnumerable<Product>> GetEntitiesByProductNames(IEnumerable<string?> products, CancellationToken token = default)
        => await context.Product.Where(_ => products.Contains(_.Uri.Product)).ToArrayAsync();

    public async Task<IEnumerable<Product>> GetEntitiesByVendorNames(IEnumerable<string?> vendors, CancellationToken token = default)
        => await context.Product.Where(_ => vendors.Contains(_.Uri.Vendor)).ToArrayAsync();

    public override async Task<Product?> GetAsync(string id, CancellationToken token)
    {
        var product = await context
        .Product
        .AsNoTracking()
        .Where(_ => _.Id == id)
        .Include(p => p.References)
        .Include(p => p.Vulnerabilities)
        .FirstOrDefaultAsync(token);

        if (product == null) return product;

        var queryable = context.Product.AsNoTracking().AsQueryable().Where(p => p.Id == product.Id);
        var ANY = "*";
        var NOT_APPLICABLE = "-";

        if (product.Uri.Vendor != ANY && product.Uri.Vendor != NOT_APPLICABLE)
            queryable = queryable.Where(p => p.Uri.Vendor!.StartsWith(product.Uri.Vendor!));
        if (product.Uri.Product != ANY && product.Uri.Product != NOT_APPLICABLE)
            queryable = queryable.Where(p => p.Uri.Product!.StartsWith(product.Uri.Product!));
        if (product.Uri.Version != ANY && product.Uri.Version != NOT_APPLICABLE)
            queryable = queryable.Where(p => p.Uri.Version!.StartsWith(product.Uri.Version!));
        if (product.Uri.Update != ANY && product.Uri.Update != NOT_APPLICABLE)
            queryable = queryable.Where(p => p.Uri.Update!.StartsWith(product.Uri.Update!));
        if (product.Uri.Edition != ANY && product.Uri.Edition != NOT_APPLICABLE)
            queryable = queryable.Where(p => p.Uri.Edition!.StartsWith(product.Uri.Edition!));
        if (product.Uri.SoftwareEdition != ANY && product.Uri.SoftwareEdition != NOT_APPLICABLE)
            queryable = queryable.Where(p => p.Uri.SoftwareEdition!.StartsWith(product.Uri.SoftwareEdition!));
        if (product.Uri.TargetSoftware != ANY && product.Uri.TargetSoftware != NOT_APPLICABLE)
            queryable = queryable.Where(p => p.Uri.TargetSoftware!.StartsWith(product.Uri.TargetSoftware!));
        if (product.Uri.TargetHardware != ANY && product.Uri.TargetHardware != NOT_APPLICABLE)
            queryable = queryable.Where(p => p.Uri.TargetHardware!.StartsWith(product.Uri.TargetHardware!));
        if (product.Uri.Language != ANY && product.Uri.Language != NOT_APPLICABLE)
            queryable = queryable.Where(p => p.Uri.Language!.StartsWith(product.Uri.Language!));
        if (product.Uri.Other != ANY && product.Uri.Other != NOT_APPLICABLE)
            queryable = queryable.Where(p => p.Uri.Other!.StartsWith(product.Uri.Other!));

        var vulnerabilities = await queryable.SelectMany(p => p.Vulnerabilities).ToListAsync(token);
        product.Vulnerabilities = product.Vulnerabilities.Concat(vulnerabilities).DistinctBy(v => v.Id).ToList();
        return product;
    }

    public async Task<SearchResult<String>> TypeaheadVendorsAsync(string? phrase, int size, CancellationToken token)
    {
        var original = phrase;
        if (phrase == null) phrase = "*";
        else phrase = $"{phrase}*";
        var inTextPhrase = $"*{phrase}";
        var matches = await elastic.SearchAsync<ProductDocument>(s => s
            .Size(size)
            .Collapse(f => f.Field(p => p.Vendor))
            .Query(q => q
                .Bool(b => b
                    .MinimumShouldMatch(1)
                    .Should(
                        s => s.Term(t => t.Field(p => p.Vendor).CaseInsensitive().Value(original).Boost(100)),
                        s => s.Wildcard(w => w.Field(p => p.Vendor).CaseInsensitive().Value(phrase).Boost(10)),
                        s => s.Wildcard(w => w.Field(p => p.Vendor).CaseInsensitive().Value(inTextPhrase))
                    )
                )
            )
        , token);
        return new(matches.Documents.Select(_ => _.Vendor!), matches.Total);
    }

    public async Task<SearchResult<String>> TypeaheadVendorProductsAsync(string vendor, string? phrase, int size, CancellationToken token)
    {
        var original = phrase;
        if (phrase == null) phrase = "*";
        else phrase = $"*{phrase}*";
        var matches = await elastic.SearchAsync<ProductDocument>(s => s
            .Size(size)
            .Collapse(f => f.Field(p => p.Product))
            .Query(q => q
                .Bool(b => b
                    .Filter(f => f
                        .Term(p => p.Vendor, vendor)
                    )
                    .Should(
                        s => s.Term(t => t
                            .Field(p => p.Product)
                            .Boost(10)
                            .Value(original)
                            .CaseInsensitive()),
                        s => s.Prefix(t => t
                            .Field(p => p.Product)
                            .Boost(2)
                            .Value(original)
                            .CaseInsensitive()),
                        s => s.Wildcard(w => w
                            .Field(p => p.Product)
                            .Value(phrase)
                            .CaseInsensitive()
                        )
                    )
                    .MinimumShouldMatch(1)
                )
            )
        , token);
        return new(matches.Documents.Select(_ => _.Product!), matches.Total);
    }

    public async Task<SearchResult<String>> TypeaheadVendorProductVersionsAsync(string vendor, string product, string phrase, int size, CancellationToken token)
    {
        if (phrase == null) phrase = "*";
        else phrase = $"*{phrase}*";
        var matches = await elastic.SearchAsync<ProductDocument>(s => s
            .Size(size)
            .Query(q => q
                .Bool(b => b
                    .Filter(
                        f => f.Term(p => p.Vendor, vendor),
                        f => f.Term(p => p.Product, product)
                    )
                    .Must(q => q
                        .Wildcard(w => w
                            .Field(p => p.Version)
                            .Value(phrase)
                            .CaseInsensitive()
                        )
                    )
                )
            )
        , token);
        return new(matches.Documents.Select(_ => _.Version!).Distinct(), matches.Total);
    }
}