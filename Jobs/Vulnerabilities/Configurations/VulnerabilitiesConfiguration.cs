using Vulns.Core;
using Vulns.Jobs.Base;

namespace Vulns.Jobs.Vulnerabilities;
public class VulnerabilitiesConfiguration : JobConfiguration<Vulnerability>
{
    public string Url { get; set; } = "https://nvd.nist.gov/feeds/json/cve/1.1/nvdcve-1.1-{YEAR}.json.zip";
    public override string Name { get; set; } = nameof(VulnerabilitiesJob);
    public override IEnumerable<object>? PartialFetchingParts { get; set; }
    public string UpdatesEndpoint { get; set; } = "https://services.nvd.nist.gov/rest/json/cves/1.0/";
    public string ApiKey { get; set; } = "fe391c99-f1a8-4ec4-a3b8-a6fc43e464aa";
    public int UpdatesSize { get; set; } = 2000;
    public override TimeSpan UpdateInterval { get; set; } = TimeSpan.FromMinutes(5);

    public VulnerabilitiesConfiguration()
    {
        var parts = new List<object>();
#if DEBUG
        foreach (var year in Directory.GetDirectories("Debug/"))
            parts.Add(int.Parse(year.Substring(6)));
#else
        for (int i = 2002; i <= DateTime.UtcNow.Year; i++)
            parts.Add(i);
#endif
        PartialFetchingParts = parts;
    }
}