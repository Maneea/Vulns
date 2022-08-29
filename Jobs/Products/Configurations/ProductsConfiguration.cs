using Vulns.Core;
using Vulns.Jobs.Base;

namespace Vulns.Jobs.Products;
public class ProductsConfiguration : JobConfiguration<Product>
{
    public string Url { get; set; } = "https://nvd.nist.gov/feeds/xml/cpe/dictionary/official-cpe-dictionary_v2.3.xml.zip";
    public override string Name { get; set; } = nameof(ProductsJob);
    
    public string UpdatesEndpoint { get; set; } = "https://services.nvd.nist.gov/rest/json/cpes/1.0/";
    public string ApiKey { get; set; } = "fe391c99-f1a8-4ec4-a3b8-a6fc43e464aa";
    public int UpdatesSize { get; set; } = 2000;
    public override TimeSpan UpdateInterval { get; set; } = TimeSpan.FromMinutes(10);
}