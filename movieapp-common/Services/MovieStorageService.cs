using Azure.Core;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using movieapp_common.Settings;
using System.Text;

namespace movieapp_common.Services;

/// <summary>
/// Storage class for storing imported movies.
/// </summary>
public class MovieStorageService
{
    private readonly ILogger _logger;
    private readonly BlobServiceSettings _blobServiceSettings;
    private const string ContainerName = "source";

    public MovieStorageService(ILoggerFactory loggerFactory, IOptions<BlobServiceSettings> blobServiceSettings)
    {
        _logger = loggerFactory.CreateLogger<MovieStorageService>();
        _blobServiceSettings = blobServiceSettings.Value;
    }

    public async Task<bool> SaveAsync(string content)
    {
        var containerClient = GetContainerClient(ContainerName);
        var blobClient = containerClient.GetBlobClient("/movies.json");

        using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(content)))
        {
            await blobClient.DeleteIfExistsAsync();
            await blobClient.UploadAsync(ms);

            return true;
        }
    }

    private BlobContainerClient GetContainerClient(string containerName)
    {
        var blobOptions = new BlobClientOptions()
        {
            Retry =
            {
                Delay = TimeSpan.FromSeconds(2),
                MaxRetries = 5,
                Mode = RetryMode.Exponential,
                MaxDelay = TimeSpan.FromSeconds(10),
                NetworkTimeout = TimeSpan.FromSeconds(100),
            },
        };

        return new BlobContainerClient(_blobServiceSettings.ConnectionString, containerName, blobOptions);
    }
}
