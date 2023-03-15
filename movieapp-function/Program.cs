using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using movieapp_common.Services;
using movieapp_common.Settings;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        // Settings
        services.AddOptions<BlobServiceSettings>()
            .Configure<IConfiguration>((settings, configuration) =>
                configuration.GetSection(BlobServiceSettings.SectionKey)
                .Bind(settings));

        services.AddOptions<TheMovieDatabaseSettings>()
            .Configure<IConfiguration>((settings, configuration) =>
                configuration.GetSection(TheMovieDatabaseSettings.SectionKey)
                .Bind(settings));

        // Services
        services.AddScoped<TheMovieDatabaseApi>();
        services.AddScoped<MovieStorageService>();

        services.AddLogging();
    })
    .Build();

host.Run();
