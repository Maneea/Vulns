using System.Globalization;
using System.IO.Compression;

using AutoMapper;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;

using Vulns.Core;
using Vulns.Infrastructure;
using Vulns.Jobs.Base;

namespace Vulns.Jobs.Vulnerabilities;
public class VulnerabilitiesJob : Job<Vulnerability>
{
    private readonly ILogger<VulnerabilitiesJob> _logger;
    private readonly TemporaryStorageService _temporaryStorage;
    private readonly HttpClient _client;
    private readonly IMapper _mapper;
    private readonly IOptions<VulnerabilitiesConfiguration> _options;

    public VulnerabilitiesJob(
        ILogger<VulnerabilitiesJob> logger,
        TemporaryStorageService temporaryStorage,
        HttpClient client,
        IMapper mapper,
        IServiceProvider serviceProvider,
        IOptions<VulnerabilitiesConfiguration> options
        ) : base(serviceProvider, options)
    {
        _logger = logger;
        _temporaryStorage = temporaryStorage;
        _client = client;
        _mapper = mapper;
        _options = options;
    }

    protected override async Task<IEnumerable<Vulnerability>> DownloadInitializationContentAsync(CancellationToken token, object? part)
    {
        _logger.LogInformation($"{name}.{nameof(DownloadInitializationContentAsync)} Routine Instatiated");
#if DEBUG
        _logger.LogDebug($"{name}.{nameof(DownloadInitializationContentAsync)} Importing files from the Debug/ folder");
        await Task.Delay(1);
        var file = @$"Debug/vulnerabilities-2022.json";
#else
        if (part == null) throw new Exception("Initalizing vulnerabilities must be done in parts.");
        var year = (int)part;
        var dir = _temporaryStorage.CreateTemporaryDirectory();
        var response = await _client.GetAsync($"{_options.Value.Url.Replace("{YEAR}", year.ToString())}");
        Stream stream = await response.Content.ReadAsStreamAsync();
        ZipFileExtensions.ExtractToDirectory(new ZipArchive(stream), dir, true);
        var file = Directory.GetFiles(dir).First();
#endif
        var content = File.ReadAllText(file);
        NvdFeedRoot nvdFeedRoot = JsonConvert.DeserializeObject<NvdFeedRoot>(content)!;
        return _mapper.Map<List<Vulnerability>>(nvdFeedRoot.Items);
    }

    protected override async Task<IEnumerable<Vulnerability>> DownloadUpdateContentAsync(DateTime timestamp, CancellationToken token)
    {
        DateTime from = timestamp.ToUniversalTime().AddMinutes(1);
        DateTime to;
        IEnumerable<Vulnerability> updates = new List<Vulnerability>();
        do
        {
            to = from.ToUniversalTime().AddDays(100);
            updates = updates.Concat(await DownloadUpdateContentInTimeRangeAsync(from, to, token));
            from = to.AddMinutes(1);
        } while (to < DateTime.UtcNow);
        return updates.DistinctBy(v => v.Id);
    }

    private async Task<IEnumerable<Vulnerability>> DownloadUpdateContentInTimeRangeAsync(DateTime from, DateTime to, CancellationToken token)
    {
        _logger.LogInformation($"Got into {name}.{nameof(DownloadUpdateContentAsync)}");
        int startFrom = 0;
        IEnumerable<Vulnerability> updates = new List<Vulnerability>();
        NvdCveUpdateFeed feeds;
        do
        {
            var endpoint = ApplyQueryParametersToUpdatesEndpoint(from, to, _options.Value.UpdatesEndpoint, startFrom);
            string response = await _client.GetStringAsync(endpoint);
            feeds = JsonConvert.DeserializeObject<NvdCveUpdateFeed>(response)!;
            if (feeds.NvdCveData != null && feeds.NvdCveData.Items.Any())
                updates = updates.Concat(_mapper.Map<List<Vulnerability>>(feeds.NvdCveData.Items));
            startFrom += _options.Value.UpdatesSize;
        } while (feeds.HasMoreData());
        return updates;
    }

    private string ApplyQueryParametersToUpdatesEndpoint(DateTime from, DateTime to, string endpoint, int startFrom = 0)
    {
        var dateTimeFormat = @"yyyy-MM-ddTHH:mm:ss:fff\%20%K";
        var fromString = from.ToUniversalTime().ToString(dateTimeFormat, CultureInfo.InvariantCulture);
        var toString = to.ToUniversalTime().ToString(dateTimeFormat, CultureInfo.InvariantCulture);

        endpoint += @$"?apiKey={_options.Value.ApiKey}";
        endpoint += @$"&resultsPerPage={_options.Value.UpdatesSize}";
        endpoint += @$"&startIndex={startFrom}";
        endpoint += @$"&modStartDate={fromString}";
        endpoint += @$"&modEndDate={toString}";
        endpoint += @$"&includeMatchStringChange=true";
        return endpoint;
    }
}
