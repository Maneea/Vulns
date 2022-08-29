using System.Globalization;
using System.IO.Compression;
using System.Xml.Serialization;

using AutoMapper;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;

using Vulns.Core;
using Vulns.Infrastructure;
using Vulns.Jobs.Base;

namespace Vulns.Jobs.Products;
public class ProductsJob : Job<Product>
{
    private readonly ILogger<ProductsJob> _logger;
    private readonly TemporaryStorageService _temporaryStorage;
    private readonly HttpClient _client;
    private readonly IMapper _mapper;
    private readonly IOptions<ProductsConfiguration> _options;

    public ProductsJob(
        ILogger<ProductsJob> logger,
        TemporaryStorageService temporaryStorage,
        HttpClient client,
        IMapper mapper,
        IServiceProvider serviceProvider,
        IOptions<ProductsConfiguration> options
        ) : base(serviceProvider, options)
    {
        _logger = logger;
        _temporaryStorage = temporaryStorage;
        _client = client;
        _mapper = mapper;
        _options = options;
    }

    protected override async Task<IEnumerable<Product>> DownloadInitializationContentAsync(CancellationToken token, object? part = null)
    {
        _logger.LogInformation($"{name}.{nameof(DownloadInitializationContentAsync)} Routine Instatiated");
#if DEBUG
        _logger.LogDebug($"{name}.{nameof(DownloadInitializationContentAsync)} Importing files from the Debug/ folder");
        await Task.Delay(1);
        var file = @"Debug/products.xml";
#else
        var dir = _temporaryStorage.CreateTemporaryDirectory();
        HttpResponseMessage responseMessage = await new HttpClient().GetAsync(_options.Value.Url, token);
        Stream stream = await responseMessage.Content.ReadAsStreamAsync();
        ZipFileExtensions.ExtractToDirectory(new ZipArchive(stream), dir, true);
        var file = Directory.GetFiles(dir).First();
#endif
        XmlSerializer serializer = new XmlSerializer(typeof(NvdCpeItems));
        FileStream fs = new FileStream(file, FileMode.Open);
        NvdCpeItems? items = (NvdCpeItems?)serializer.Deserialize(fs);
        List<Product> cpeList = new();
        if (items == null)
            throw new Exception();
        cpeList = _mapper.Map<List<Product>>(items.ItemList);
        cpeList.ForEach(p => p.CreatedAt = items.Generator.Timestamp);
        return cpeList; 
    }


    protected override async Task<IEnumerable<Product>> DownloadUpdateContentAsync(DateTime timestamp, CancellationToken token)
    {
        timestamp = timestamp.AddMinutes(1);
        _logger.LogInformation($"Got into {name}.{nameof(DownloadUpdateContentAsync)}");
        int startFrom = 0;
        IEnumerable<Product> updates = new List<Product>();
        NvdCpeUpdateFeedsRoot feeds;
        do
        {
            var endpoint = ApplyQueryParametersToUpdatesEndpoint(timestamp, _options.Value.UpdatesEndpoint, startFrom);
            string response = await _client.GetStringAsync(endpoint);
            feeds = JsonConvert.DeserializeObject<NvdCpeUpdateFeedsRoot>(response)!;
            if (feeds.NvdCpeData != null && feeds.NvdCpeData.Cpes != null && feeds.NvdCpeData.Cpes.Any())
                updates = updates.Concat(_mapper.Map<List<Product>>(feeds.NvdCpeData.Cpes));
            startFrom += _options.Value.UpdatesSize;
        } while (feeds.HasMoreData());
        return updates;
    }

   private string ApplyQueryParametersToUpdatesEndpoint(DateTime timestamp, string endpoint, int startFrom = 0)
    {
        var dateTimeFormat = @"yyyy-MM-ddTHH:mm:ss:fff\%20%K";
        var from = timestamp.ToUniversalTime().ToString(dateTimeFormat, CultureInfo.InvariantCulture);
        var to = DateTime.UtcNow.ToString(dateTimeFormat, CultureInfo.InvariantCulture);

        endpoint += @$"?apiKey={_options.Value.ApiKey}";
        endpoint += @$"&resultsPerPage={_options.Value.UpdatesSize}";
        endpoint += @$"&startIndex={startFrom}";
        endpoint += @$"&modStartDate={from}";
        endpoint += @$"&modEndDate={to}";
        return endpoint;
    }
}
