using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using movieapp_backend.Extensions;
using movieapp_backend.Features.Movies;
using movieapp_common.Settings;
using System.Reflection;

var corsPolicyName = "CorsPolicy";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddOptions<MovieDatabaseSettings>()
            .Configure<IConfiguration>((settings, configuration) =>
                configuration.GetSection(MovieDatabaseSettings.SectionKey)
                .Bind(settings));

builder.Services.AddDbContext<MovieDbContext>(
        (serviceProvider, contextOptions) =>
        {
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            var connectionOptions = serviceProvider.GetRequiredService<IOptionsMonitor<MovieDatabaseSettings>>();            

            contextOptions
                .UseSqlServer(connectionOptions.CurrentValue.ConnectionString)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

#if DEBUG
            if (Environment.GetEnvironmentVariable("IsSensitiveDataLoggingEnabled") == "true")
            {
                contextOptions.EnableSensitiveDataLogging();
            }
#endif
        });

// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cors setup for local testing
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: corsPolicyName, builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(corsPolicyName);

// Map custom api endpoints
app.MapMovieEndpoints();

app.Run();
