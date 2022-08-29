using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Nest;

using Vulns.Core;

namespace Vulns.Infrastructure;
public static class Startup
{
    public static void Configure(IServiceCollection srv, IConfiguration conf)
    {
        ConfigurePersistence(srv, conf);
        ConfigureRepositories(srv, conf);
        srv.AddSingleton<TemporaryStorageService>();
    }

    private static void ConfigureRepositories(IServiceCollection srv, IConfiguration conf)
    {
        srv.AddScoped<VulnerabilityRepository>();
        srv.AddScoped<Core.IRepository<Vulnerability>, VulnerabilityRepository>();
        srv.AddScoped<Core.ISearchableRepository<Vulnerability>, VulnerabilityRepository>();

        srv.AddScoped<WeaknessRepository>();
        srv.AddScoped<Core.IRepository<Weakness>, WeaknessRepository>();
        srv.AddScoped<Core.ISearchableRepository<Weakness>, WeaknessRepository>();

        srv.AddScoped<IssuerRepository>();
        srv.AddScoped<Core.IRepository<Issuer>, IssuerRepository>();
        srv.AddScoped<Core.ISearchableRepository<Issuer>, IssuerRepository>();

        srv.AddScoped<ProductRepository>();
        srv.AddScoped<Core.IRepository<Product>, ProductRepository>();
        srv.AddScoped<Core.ISearchableRepository<Product>, ProductRepository>();
    }

    private static void ConfigurePersistence(IServiceCollection srv, IConfiguration conf)
    {
        var cs = conf.GetConnectionString("MySql");
        srv.AddDbContext<AppDbContext>(b =>
        {
            b.UseMySql(cs, ServerVersion.AutoDetect(cs), o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
            #if DEBUG
            b.EnableSensitiveDataLogging();
            b.EnableDetailedErrors();
            #endif
        });
        var client = ElasticsearchConfiguration.Configure(conf.GetSection("Elasticsearch").GetChildren());
        srv.AddSingleton<IElasticClient>(client);
    }
}