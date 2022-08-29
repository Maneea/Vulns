Host.CreateDefaultBuilder(args)
    .ConfigureLogging((builder, logging) => Startup.ConfigureLogging(builder, logging))
    .ConfigureAppConfiguration((builder, configurations) => Startup.ConfigureOptions(builder, configurations))
    .ConfigureServices((builder, services) => Startup.ConfigureServices(builder, services))
    .Build()
    .Run();