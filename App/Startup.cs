using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Vulns.App;
public static class Startup
{
    public static void Configure(IServiceCollection srv, IConfiguration conf)
    {
        ConfigureAppServices(srv, conf);
    }

    private static void ConfigureAppServices(IServiceCollection srv, IConfiguration conf)
    {
        srv.AddScoped<VulnerabilityAggregationService>();
        srv.AddScoped<VulnerabilitySearchService>();
        srv.AddScoped<ProductSearchService>();
        srv.AddScoped(typeof(SearchService<>));
    }
}