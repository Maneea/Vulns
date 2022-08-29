using Microsoft.Extensions.Configuration;

using Nest;

using Vulns.Core;

namespace Vulns.Infrastructure;
public static class ElasticsearchConfiguration
{
    public static IElasticClient Configure(IEnumerable<IConfigurationSection> conf)
        => new ConnectionSettings(new Uri(conf.First(c => c.Key == "Endpoint").Value))
#if DEBUG
            .DisableDirectStreaming()
#endif
            .BasicAuthentication(conf.First(c => c.Key == "Username").Value, conf.First(c => c.Key == "Password").Value)
            .CertificateFingerprint(conf.First(c => c.Key == "Fingerprint").Value)
            .IgnoreCoreDomainModelsProperties()
            .GenerateClient()
            .MapCoreDomainModels();

    public static ConnectionSettings IgnoreCoreDomainModelsProperties(this ConnectionSettings connection)
        => connection
            .DefaultMappingFor<IssuerDocument>(m => m
                .IndexName(nameof(Issuer).ToSnakeCase()))
            .DefaultMappingFor<WeaknessDocument>(m => m
                .IndexName(nameof(Weakness).ToSnakeCase()))
            .DefaultMappingFor<ProductDocument>(m => m
                .IndexName(nameof(Product).ToSnakeCase()))
            .DefaultMappingFor<VulnerabilityDocument>(m => m
                .IndexName(nameof(Vulnerability).ToSnakeCase()));

    public static ElasticClient GenerateClient(this ConnectionSettings settings) => new ElasticClient(settings);

    public static ElasticClient MapCoreDomainModels(this ElasticClient client)
    {
        client.Indices.Create(nameof(Issuer).ToSnakeCase(), c => c.Map<IssuerDocument>(m => m.AutoMap()));
        client.Indices.Create(nameof(Weakness).ToSnakeCase(), c => c.Map<WeaknessDocument>(m => m.AutoMap()));
        client.Indices.Create(nameof(Vulnerability).ToSnakeCase(), c => c
            .Settings(s => s.Setting("index.max_result_window", 1_000_000.ToString())).Map<VulnerabilityDocument>(m => m.AutoMap()));
        client.Indices.Create(nameof(Product).ToSnakeCase(), c => c
            .Settings(s => s.Setting("index.max_result_window", 2_000_000.ToString())).Map<ProductDocument>(m => m.AutoMap()));
        return client;
    }
}