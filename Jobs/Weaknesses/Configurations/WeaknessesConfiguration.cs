using Vulns.Core;
using Vulns.Jobs.Base;

namespace Vulns.Jobs.Weaknesses;
public class WeaknessesConfiguration : JobConfiguration<Weakness>
{
    public string Url { get; set; } = "https://cwe.mitre.org/data/xml/views/2000.xml.zip";
    public override string Name { get; set; } = nameof(WeaknessesJob);
}