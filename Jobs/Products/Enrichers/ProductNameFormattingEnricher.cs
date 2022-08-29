using Microsoft.Extensions.DependencyInjection;

using Vulns.Core;
using Vulns.Infrastructure;
using Vulns.Jobs.Base;

namespace Vulns.Jobs.Products;
public class ProductNameFormattingEnricher : IEnricher<Product>
{
    public int ExecutionOrder => 1;

    public string Name => nameof(ProductNameFormattingEnricher);

    public IEnumerable<Product> Enrich(IEnumerable<Product> entities, IServiceScope scope)
    {
        var repo = scope.ServiceProvider.GetRequiredService<ProductRepository>();
        EnrichFromSelf(entities);
        EnrichFromRepo(entities, repo);
        return entities;
    }

    private void EnrichFromRepo(IEnumerable<Product> entities, ProductRepository repo)
    {
        var productsWithoutTitles = entities.Where(_ => string.IsNullOrEmpty(_.Title));
        var productNamesWithoutTitles = productsWithoutTitles.Select(_ => _.Uri.Product);
        var titlefulls = repo.GetEntitiesByProductNames(productNamesWithoutTitles).Result;
        foreach (var product in productsWithoutTitles)
        {
            var titlefull = titlefulls.Where(_ => _.Uri.Product == product.Uri.Product).FirstOrDefault();
            if (titlefull == null) break;
            product.Uri.ApplyFormatting(titlefull.Title!);
        }

        productsWithoutTitles = entities.Where(_ => string.IsNullOrEmpty(_.Title));
        var vendorsNamesWithoutTitles = productsWithoutTitles.Select(_ => _.Uri.Vendor);
        titlefulls = repo.GetEntitiesByVendorNames(vendorsNamesWithoutTitles).Result;
        foreach (var product in productsWithoutTitles)
        {
            var titlefull = titlefulls.Where(_ => _.Uri.Vendor == product.Uri.Vendor).FirstOrDefault();
            if (titlefull == null) break;
            product.Uri.ApplyFormatting(titlefull.Title!);
        }
    }

    private void EnrichFromSelf(IEnumerable<Product> entities)
    {
        foreach (var titleless in entities.Where(_ => string.IsNullOrEmpty(_.Title)))
        {
            var titlefulls = EntitiesWithSimilarVendorAndProduct(titleless, entities);
            if (!titlefulls.Any()) titlefulls = EntitiesWithSimilarVendor(titleless, entities);
            if (!titlefulls.Any()) break;
            else titleless.Uri.ApplyFormatting(titlefulls.First().Title!);
        }
    }
    
    private IEnumerable<Product> EntitiesWithSimilarVendorAndProductAndVersion(Product entity, IEnumerable<Product> entities)
        => entities
            .Where(_ => _.Uri.Vendor == entity.Uri.Vendor)
            .Where(_ => _.Uri.Product == entity.Uri.Product)
            .Where(_ => _.Uri.Version == entity.Uri.Version)
            .Where(_ => !string.IsNullOrEmpty(_.Title));

    private IEnumerable<Product> EntitiesWithSimilarVendorAndProduct(Product entity, IEnumerable<Product> entities)
        => entities
            .Where(_ => _.Uri.Vendor == entity.Uri.Vendor)
            .Where(_ => _.Uri.Product == entity.Uri.Product)
            .Where(_ => !string.IsNullOrEmpty(_.Title));

    private IEnumerable<Product> EntitiesWithSimilarVendor(Product entity, IEnumerable<Product> entities)
        => entities
            .Where(_ => _.Uri.Vendor == entity.Uri.Vendor)
            .Where(_ => !string.IsNullOrEmpty(_.Title));
}