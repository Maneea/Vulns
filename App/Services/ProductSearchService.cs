using Vulns.Core;
using Vulns.Infrastructure;

namespace Vulns.App;

public class ProductSearchService : SearchService<Product>
{
    private readonly ProductRepository _repo;

    public ProductSearchService(ProductRepository repo) : base(repo) => _repo = repo;

    public async Task<SearchResult<String>> TypeaheadVendorsAsync(string? vendor, int size, CancellationToken token)
        => await _repo.TypeaheadVendorsAsync(vendor, size, token);

    public async Task<SearchResult<String>> TypeaheadVendorProductsAsync(string vendor, string? product, int size, CancellationToken token)
        => await _repo.TypeaheadVendorProductsAsync(vendor, product, size, token);

    public async Task<SearchResult<String>> TypeaheadVendorProductVersionsAsync(string vendor, string product, string version, int size, CancellationToken token)
        => await _repo.TypeaheadVendorProductVersionsAsync(vendor, product, version, size, token);
}