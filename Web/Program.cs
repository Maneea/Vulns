using Vulns.Web;

var builder = WebApplication.CreateBuilder(args);
Vulns.Web.Startup.Configure(builder);
Vulns.App.Startup.Configure(builder.Services, builder.Configuration);
Vulns.Infrastructure.Startup.Configure(builder.Services, builder.Configuration);
var app = builder.Build();

await Vulns.Infrastructure.AppDbContextInitializer.Seed(app.Services);

app.BuildMiddlewarePipeline(builder);
app.Run();