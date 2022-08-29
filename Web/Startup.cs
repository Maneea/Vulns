using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

using Vulns.Infrastructure;
using Vulns.Services;

namespace Vulns.Web;
public static class Startup
{
    public static WebApplicationBuilder Configure(WebApplicationBuilder builder)
    {
        ConfigureEndpoints(builder);
        ConfigureOptions(builder);
        ConfigureLogging(builder);
        ConfigureIdentity(builder);
        ConfigureMappings(builder);
        ConfigureDevelopment(builder);
        ConfigureSpa(builder);
        return builder;
    }

    public static WebApplication BuildMiddlewarePipeline(this WebApplication app, WebApplicationBuilder builder)
    {
        // TODO: remove development permissive CORS
        app.UseCors(b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
        app.UseLogoRedirectionMiddleware(builder)
            .UseLogosFileServer(builder)
            .UseHttpsRedirection()
            .UseRouting()
            .UseAuthentication()
            .UseAuthorization()
            .UseSwagger()
            .UseSwaggerUI()
            .UseEndpoints(e => e.MapControllers())
            .UseSpaStaticFiles();
        app.UseSpa(c => { });
        return app;
    }

    private static void ConfigureSpa(WebApplicationBuilder builder)
    {
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        var spaFolder = Path.Combine(baseDir, "Static/Spa");
        builder.Services.AddSpaStaticFiles(c => c.RootPath = spaFolder);
    }

    private static void ConfigureEndpoints(WebApplicationBuilder builder)
    {
        builder.Services.Configure<ApiBehaviorOptions>(o => o.SuppressModelStateInvalidFilter = true);
        builder.Services.AddControllers().AddJsonOptions(o =>
        {
            o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        })
        .ConfigureApiBehaviorOptions(opt => opt.SuppressMapClientErrors = true);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(o =>
        {
            o.SwaggerDoc("v1", new OpenApiInfo { Title = "VulnView API", Version = "v1" });
            o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            o.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            o.IncludeXmlComments(
               Path.Combine(
                   AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"
               )
           );
        });
    }

    private static void ConfigureOptions(WebApplicationBuilder builder)
    {
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        var envName = builder.Environment.EnvironmentName;
        var envPostfix = envName == "Production" ? string.Empty : $".{envName}";
        var configFile = Path.Combine(baseDir, $"appsettings{envPostfix}.json");
        builder.Configuration.AddJsonFile(configFile, optional: false, reloadOnChange: true);
    }

    private static void ConfigureLogging(WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Host.UseSerilog((hostBuildCtx, loggerConf) => loggerConf
            // .MinimumLevel.Verbose()
            // .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
            // .MinimumLevel.Override("System", LogEventLevel.Information)
            .Enrich.WithMemoryUsage()
            .WriteTo.Console(
                theme: AnsiConsoleTheme.Code,
                outputTemplate: "[{Timestamp:yy-MM-dd HH:mm:ss}] " +
                    "[{Level,-11}] " +
                    "[{MemoryUsage} bytes] " +
                    "{Message:lj} - {NewLine}{Exception}"
            )
        );
    }

    private static void ConfigureIdentity(WebApplicationBuilder builder)
    {
        var identityBuilder = builder.Services
            .AddIdentity<User, IdentityRole>()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<AppDbContext>();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = builder.Configuration["JWT:ValidAudience"],
                ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
            };
        });

        builder.Services.AddAuthorization();
        builder.Services.AddSingleton<JwtService>();
    }

    private static void ConfigureMappings(WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }

    private static void ConfigureDevelopment(WebApplicationBuilder builder)
    {
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddCors();
        }
    }

    private static WebApplication UseLogoRedirectionMiddleware(this WebApplication app, WebApplicationBuilder builder)
    {
        app.UseRewriter(new RewriteOptions()
            .AddRewrite(@"^logos/issuers/(.*)", @"logos/Issuers/$1", true)
            .AddRewrite(@"^logos/vendors/(.*)", @"logos/Vendors/$1", true))
            .Use(async (context, next) =>
            {
                var path = context.Request.Path.Value;
                if (!string.IsNullOrEmpty(path) && path.StartsWith("/logos"))
                {
                    if (path == "/logos/default.png")
                    {
                        await next();
                        return;
                    }

                    var targetResources = path.Split('/');
                    if (targetResources.Length < 4)
                    {
                        context.Response.Redirect("/logos/default.png");
                        return;
                    }

                    if (!new[] { "Issuers", "Vendors" }.Contains(targetResources[2]))
                    {
                        context.Response.Redirect("/logos/default.png");
                        return;
                    }

                    var targetFile = Path.Combine(builder.Environment.ContentRootPath, $"Static/Images/{targetResources[2]}/{targetResources[3]}");
                    if (!File.Exists(targetFile))
                    {
                        context.Response.Redirect($"/logos/{targetResources[2]}/default.png");
                        return;
                    }
                    else if ((File.GetAttributes(targetFile) & FileAttributes.Normal) == 0 && (File.GetAttributes(targetFile) & FileAttributes.ReadOnly) == 0)
                    {
                        context.Response.Redirect($"/logos/{targetResources[2]}/default.png");
                        return;
                    }
                }
                await next();
            });
        return app;
    }

    private static WebApplication UseLogosFileServer(this WebApplication app, WebApplicationBuilder builder)
    {
        app.UseFileServer(new FileServerOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Static/Images")),
            RequestPath = "/logos",
        });
        return app;
    }

}