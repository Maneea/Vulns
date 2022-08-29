using System.IO.Compression;
using System.Xml.Serialization;

using AutoMapper;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Vulns.Core;
using Vulns.Infrastructure;
using Vulns.Jobs.Base;

namespace Vulns.Jobs.Weaknesses;
public class WeaknessesJob : Job<Weakness>
{
    private readonly ILogger<WeaknessesJob> _logger;
    private readonly TemporaryStorageService _temporaryStorage;
    private readonly HttpClient _client;
    private readonly IMapper _mapper;
    private readonly IServiceProvider _serviceProvider;
    private readonly IOptions<WeaknessesConfiguration> _options;

    public WeaknessesJob(
        ILogger<WeaknessesJob> logger,
        TemporaryStorageService temporaryStorage,
        HttpClient client,
        IMapper mapper,
        IServiceProvider serviceProvider,
        IOptions<WeaknessesConfiguration> options
        ) : base(serviceProvider, options)
    {
        _logger = logger;
        _temporaryStorage = temporaryStorage;
        _client = client;
        _mapper = mapper;
        _serviceProvider = serviceProvider;
        _options = options;
    }

    protected override async Task<IEnumerable<Weakness>> DownloadInitializationContentAsync(CancellationToken token, object? part = null)
    {
        _logger.LogInformation($"{name}.{nameof(DownloadInitializationContentAsync)} Routine Instatiated");
#if DEBUG
        _logger.LogDebug($"{name}.{nameof(DownloadInitializationContentAsync)} Importing files from the Debug/ folder");
        await Task.Delay(1);
        var file = @"Debug/weaknesses.xml";
#else
        var dir = _temporaryStorage.CreateTemporaryDirectory();
        HttpResponseMessage responseMessage = await _client.GetAsync(_options.Value.Url, token);
        Stream stream = await responseMessage.Content.ReadAsStreamAsync();
        ZipFileExtensions.ExtractToDirectory(new ZipArchive(stream), dir, true);
        var file = Directory.GetFiles(dir).First();
#endif
        XmlSerializer serializer = new XmlSerializer(typeof(MitreCweWeakness_Catalog));
        FileStream fs = new FileStream(file, FileMode.Open);
        MitreCweWeakness_Catalog? catalog = (MitreCweWeakness_Catalog?)serializer.Deserialize(fs);
        if (catalog == null)
            throw new Exception();
        return _mapper.Map<IEnumerable<Weakness>>(catalog.Weaknesses).ToList();
    }

    protected override async Task<IEnumerable<Weakness>> DownloadUpdateContentAsync(DateTime timestamp, CancellationToken token)
        => (await DownloadInitializationContentAsync(token))
        .Where(w => w.CreatedAt > timestamp || (w.ModifiedAt != null && w.ModifiedAt > timestamp));
}
