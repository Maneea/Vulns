using Vulns.Core;
using Vulns.Jobs.Base;

namespace Vulns.Jobs.Issuers;
public class IssuersConfiguration : JobConfiguration<Issuer>
{
    public override string Name { get; set; } = nameof(IssuersJob);
}