using Nest;
namespace Vulns.Infrastructure;

[ElasticsearchType(RelationName = "weakness-detection")]
public class WeaknessDetectionDocument
{
    [Keyword]
    public String? Method { get; set; }
    [Keyword]
    public String? Effectiveness { get; set; }
}