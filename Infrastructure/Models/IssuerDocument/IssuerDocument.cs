using Nest;
namespace Vulns.Infrastructure;
[ElasticsearchType(RelationName = "issuer")]
public class IssuerDocument
{
    [Keyword]
    public String? Id { get; set; }
    [Keyword]
    public String? Organization { get; set; }
    [Keyword]
    public String? ShortName { get; set; }
    [Keyword]
    public String? Country { get; set; }
}