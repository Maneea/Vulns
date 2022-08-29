using System.Text.RegularExpressions;

using AutoMapper;

using Hjson;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Newtonsoft.Json.Linq;

using Vulns.Core;
using Vulns.Jobs.Base;

namespace Vulns.Jobs.Issuers;
public class IssuersJob : Job<Issuer>
{
    private readonly ILogger<IssuersJob> _logger;
    private readonly HttpClient _client;
    private readonly IMapper _mapper;
    private readonly IOptions<IssuersConfiguration> _options;

    public IssuersJob(
        ILogger<IssuersJob> logger,
        HttpClient client,
        IMapper mapper,
        IServiceProvider serviceProvider,
        IOptions<IssuersConfiguration> options
        ) : base(serviceProvider, options)
    {
        _logger = logger;
        _client = client;
        _mapper = mapper;
        _options = options;
    }

    protected override async Task<IEnumerable<Issuer>> DownloadInitializationContentAsync(CancellationToken token, object? part = null)
    {
#if DEBUG
        _logger.LogDebug($"{name}.{nameof(DownloadInitializationContentAsync)} Importing files from the Debug/ folder");
        var file = @"Debug/cna-list.json";
        var jsonData = await File.ReadAllTextAsync(file, token);
#else
        string _domain = "https://www.cve.org";
        string _path = "/ProgramOrganization/CNAs";
        String response = await _client.GetStringAsync($"{_domain}{_path}");
        var matches = Regex.Matches(response, @"(?<=(src|href)=)""?[^""> ]+\.js(?=[""> ])", RegexOptions.Multiline).DistinctBy<Match, String>(match => match.ToString()).ToArray<Match>();
        string? jsonData = "";
        for (int i = 0; i < matches.Length; i++)
        {
            var nextUrl = $"{_domain}{matches[i].ToString()}";
            response = await _client.GetStringAsync(nextUrl);
            foreach (var match in Regex.Matches(response, @"(?<=JSON\.parse\(')(\\\'|[^'])+(?='\))", RegexOptions.Multiline))
                if (match.ToString() != null && match.ToString()!.Contains("CNA-1999-0001"))
                {
                    jsonData = match.ToString();
                    break;
                }
            if (!string.IsNullOrEmpty(jsonData)) break;
        }
#endif
        var content = HjsonValue.Parse(jsonData).ToString(Stringify.Formatted);
        JArray cnaJsonArray = JArray.Parse(content);
        var cnaList = new List<Issuer>();
        foreach (var item in cnaJsonArray.Children<JObject>())
            cnaList.Add(_mapper.Map<Issuer>(item));
        foreach (var cna in cnaList)
            cna.ShortName = cna.ShortName.HumanizeFrom(cna.Organization);
        return cnaList;
    }

    protected override async Task<IEnumerable<Issuer>> DownloadUpdateContentAsync(DateTime timestamp, CancellationToken token)
        => await DownloadInitializationContentAsync(token);
}
