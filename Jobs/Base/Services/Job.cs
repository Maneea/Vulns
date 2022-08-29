using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Vulns.Core;

namespace Vulns.Jobs.Base;
public abstract class Job<T> : BackgroundService where T : Entity
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<Job<T>> _logger;
    private readonly IOptions<JobConfiguration<T>> _options;
    protected string name { get => _options.Value.Name; }
    private static Mutex mut = new Mutex();

    public Job(IServiceProvider serviceProvider, IOptions<JobConfiguration<T>> options)
    {
        _serviceProvider = serviceProvider;
        _options = options;
        _logger = serviceProvider.GetRequiredService<ILogger<Job<T>>>();
    }

    protected override async Task ExecuteAsync(CancellationToken token)
    {
        _logger.LogInformation($"Started {name} backgroun service");
        while (!await PassesChecks(token))
            await Task.Delay(TimeSpan.FromSeconds(10), token);

        var skipUpdateCycle = InitializationRoutine(token);
        while (!token.IsCancellationRequested)
        {
            if (skipUpdateCycle)
                skipUpdateCycle = !skipUpdateCycle;
            else
            {
                while (!await PassesChecks(token))
                    await Task.Delay(TimeSpan.FromSeconds(10), token);
                try
                {
                    Update(token);
                }
                catch(Exception e)
                {
                    _logger.LogError(e, "An error has occured!");
                }
            }
            await Task.Delay(_options.Value.UpdateInterval, token);
        }
        _logger.LogInformation($"Cancellation requested on {name}");
    }

    private bool InitializationRoutine(CancellationToken token)
    {
        _logger.LogInformation($"{name}.{nameof(InitializationRoutine)}: attemping to lock global fetcher mutex");
        mut.WaitOne();
        _logger.LogInformation($"{name}.{nameof(InitializationRoutine)}: locked global fetcher mutex");
        _logger.LogInformation($"{name}: Checking if initalization already took place");
        if (!IsInitializedAsync(token).Result)
        {
            _logger.LogInformation($"{name}: Not initialized. Attempting initialization now");
            InitializeAsync(token).Wait();
            _logger.LogInformation($"{name}.{nameof(InitializationRoutine)}: initalization completed");
            mut.ReleaseMutex();
            _logger.LogInformation($"{name}.{nameof(InitializationRoutine)}: released global fetcher mutex");
            return true;
        }
        _logger.LogInformation($"{name}.{nameof(InitializationRoutine)}: already initialized");
        mut.ReleaseMutex();
        _logger.LogInformation($"{name}.{nameof(InitializationRoutine)}: released global fetcher mutex");
        return false;
    }

    private async Task<bool> IsInitializedAsync(CancellationToken token)
    {
        await using (var scope = _serviceProvider.CreateAsyncScope())
            return !await scope.ServiceProvider.GetRequiredService<IRepository<T>>().IsEmptyAsync(token);
    }

    private async Task InitializeAsync(CancellationToken token)
    {
        if (_options.Value.SupportsPartialFetching)
            foreach (var part in _options.Value.PartialFetchingParts!)
                await InitializePartAsync(token, part);
        else
            await InitializePartAsync(token);
    }

    private async Task InitializePartAsync(CancellationToken token, object? part = null)
    {
        _logger.LogInformation($"{name} attempting to download initialization content");
        var enumerable = await DownloadInitializationContentAsync(token, part);

        using (var scope = _serviceProvider.CreateScope())
        {
            _logger.LogInformation($"{name} attempting enrich initialization content");
            enumerable = Enrich(enumerable, scope);

            _logger.LogInformation($"{name} attempting persist initialization content");
            await PersistAsync(enumerable, scope, token);
        }
    }

    public void Update(CancellationToken token)
    {
        _logger.LogInformation($"{name}.{nameof(Update)}: attemping to lock global fetcher mutex");
        mut.WaitOne();
        _logger.LogInformation($"{name}.{nameof(Update)}: locked global fetcher mutex");
        try {
            DateTime updateFrom;
            using (var scope = _serviceProvider.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IRepository<T>>();
                updateFrom = repo.LastTimestampAsync(token).Result;
            }

            _logger.LogInformation($"{name} attempting to update content from {updateFrom.ToShortTimeString()}");
            var enumerable = DownloadUpdateContentAsync(updateFrom, token).Result;

            using (var scope = _serviceProvider.CreateScope())
            {
                _logger.LogInformation($"{name} attempting to enrich update content");
                enumerable = Enrich(enumerable, scope);

                _logger.LogInformation($"{name} attempting to persist update content");
                PersistAsync(enumerable, scope, token).Wait();
            }
        } finally {
            mut.ReleaseMutex();
            _logger.LogInformation($"{name}.{nameof(Update)}: released global fetcher mutex");
        }
    }

    private IEnumerable<T> Enrich(IEnumerable<T> enumerable, IServiceScope scope)
    {
        var enrichers = scope.ServiceProvider.GetServices<IEnricher<T>>().OrderBy(e => e.ExecutionOrder);
        _logger.LogInformation($"{name} has {enrichers.Count()} enrichers");
        foreach (var enricher in enrichers)
        {
            _logger.LogInformation($"{name} attempting to enrich with {enricher.Name}");
            enumerable = enricher.Enrich(enumerable, scope);
        }
        return enumerable;
    }

    private async Task PersistAsync(IEnumerable<T> enumerable, IServiceScope scope, CancellationToken token)
    {
        _logger.LogInformation($"{name}.{nameof(PersistAsync)} with {enumerable.Count()} entities");
        IEnumerable<string> existingIds;

        var repo = scope.ServiceProvider.GetRequiredService<IRepository<T>>();
        existingIds = await repo.GetExistingValuesByIdAsync(enumerable.Select(_ => _.Id), token);

        var newEntities = enumerable.Where(_ => !existingIds.Contains(_.Id)).ToList();
        await PersistNewContentAsync(newEntities, scope, token);

        var updatedEntities = enumerable.Where(_ => existingIds.Contains(_.Id));
        await PersistUpdatedContentAsync(updatedEntities, scope, token);
    }

    private async Task PersistNewContentAsync(IEnumerable<T> enumerable, IServiceScope scope, CancellationToken token)
    {
        foreach (var entity in enumerable) if (entity.CreatedAt == default) entity.CreatedAt = DateTime.UtcNow;
        _logger.LogInformation($"{name}.{nameof(PersistNewContentAsync)} with {enumerable.Count()} entities");
        var transactionLimit = _options.Value.MaxEntitiesPerTransaction;
        if (enumerable.Count() > transactionLimit)
        {
            _logger.LogInformation($"{name}.{nameof(PersistNewContentAsync)} Persisting batches of size {transactionLimit}");
            for (int i = 0; i < Math.Ceiling((double)enumerable.Count() / transactionLimit); i++)
            {
                _logger.LogInformation($"{name}.{nameof(PersistNewContentAsync)} Persisting batch #{i+1}");
                var entities = enumerable.Skip(i * transactionLimit).Take(transactionLimit);
                await scope.ServiceProvider.GetRequiredService<IRepository<T>>().AddManyAsync(entities, token);
            }
        }
        else
            await scope.ServiceProvider.GetRequiredService<IRepository<T>>().AddManyAsync(enumerable, token);
    }

    private async Task PersistUpdatedContentAsync(IEnumerable<T> enumerable, IServiceScope scope, CancellationToken token)
    {
        foreach (var entity in enumerable) if (!entity.ModifiedAt.HasValue) entity.ModifiedAt = DateTime.UtcNow;
        _logger.LogInformation($"{name}.{nameof(PersistUpdatedContentAsync)} with {enumerable.Count()} entities");
        var transactionLimit = _options.Value.MaxEntitiesPerTransaction;
        if (enumerable.Count() > transactionLimit)
        {
            _logger.LogInformation($"{name}.{nameof(PersistNewContentAsync)} Persisting batches of size {transactionLimit}");
            for (int i = 0; i < Math.Ceiling((double)enumerable.Count() / transactionLimit); i++)
            {
                _logger.LogInformation($"{name}.{nameof(PersistNewContentAsync)} Persisting batch #{i+1}");
                var entities = enumerable.Skip(i * transactionLimit).Take(transactionLimit);
                await scope.ServiceProvider.GetRequiredService<IRepository<T>>().UpdateManyAsync(enumerable, token);
            }
        }
        else
            await scope.ServiceProvider.GetRequiredService<IRepository<T>>().UpdateManyAsync(enumerable, token);
    }

    private async Task<bool> PassesChecks(CancellationToken token)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var checks = scope.ServiceProvider.GetServices<IJobCheck<T>>();
            _logger.LogInformation($"{name} has {checks.Count()} checks");
            foreach (var check in checks)
                if (await check.IsCheckFailingAsync(token))
                    return false;
            return true;
        }
    }

    protected abstract Task<IEnumerable<T>> DownloadInitializationContentAsync(CancellationToken token, object? part = null);

    protected abstract Task<IEnumerable<T>> DownloadUpdateContentAsync(DateTime timestamp, CancellationToken token);
}