using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

using Vulns.Core;
using Vulns.Jobs.Base;
using Vulns.Jobs.Issuers;
using Vulns.Jobs.Products;
using Vulns.Jobs.Vulnerabilities;
using Vulns.Jobs.Weaknesses;

public static class Startup
{
    public static void ConfigureOptions(HostBuilderContext builder, IConfigurationBuilder configuration)
    {
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        var envName = builder.HostingEnvironment.EnvironmentName;
        var envPostfix = envName == "Production" ? string.Empty : $".{envName}";
        var configFile = Path.Combine(baseDir, $"appsettings{envPostfix}.json");
        configuration.AddJsonFile(configFile, optional: false, reloadOnChange: true);
    }

    public static void ConfigureLogging(HostBuilderContext builder, ILoggingBuilder logBuilder)
    {
        logBuilder.ClearProviders();
        var template = @"[{Timestamp:yy-MM-dd HH:mm:ss}] [{Level,-11}] [{MemoryUsage} bytes] {Message:lj} - {NewLine}{Exception}";
        logBuilder.AddSerilog(
            new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                .MinimumLevel.Override("System", LogEventLevel.Information)
                .Enrich.WithMemoryUsage()
                .WriteTo.Console(theme: AnsiConsoleTheme.Code, outputTemplate: template)
                .CreateLogger()
        );
    }

    public static void ConfigureServices(HostBuilderContext builder, IServiceCollection services)
    {
        Vulns.Infrastructure.Startup.Configure(services, builder.Configuration);
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddHttpClient();
        services.AddHostedService<WeaknessesJob>();
        services.AddHostedService<IssuersJob>();
        services.AddHostedService<ProductsJob>();
        services.AddHostedService<VulnerabilitiesJob>();
        foreach (var t in AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.DefinedTypes))
            if (typeof(IEnricher<Weakness>).IsAssignableFrom(t)) services.AddScoped(typeof(IEnricher<Weakness>), t);
            else if (typeof(IJobCheck<Weakness>).IsAssignableFrom(t)) services.AddScoped(typeof(IJobCheck<Weakness>), t);
            else if (typeof(IEnricher<Issuer>).IsAssignableFrom(t)) services.AddScoped(typeof(IEnricher<Issuer>), t);
            else if (typeof(IJobCheck<Issuer>).IsAssignableFrom(t)) services.AddScoped(typeof(IJobCheck<Issuer>), t);
            else if (typeof(IEnricher<Product>).IsAssignableFrom(t)) services.AddScoped(typeof(IEnricher<Product>), t);
            else if (typeof(IJobCheck<Product>).IsAssignableFrom(t)) services.AddScoped(typeof(IJobCheck<Product>), t);
            else if (typeof(IEnricher<Vulnerability>).IsAssignableFrom(t)) services.AddScoped(typeof(IEnricher<Vulnerability>), t);
            else if (typeof(IJobCheck<Vulnerability>).IsAssignableFrom(t)) services.AddScoped(typeof(IJobCheck<Vulnerability>), t);
    }
}