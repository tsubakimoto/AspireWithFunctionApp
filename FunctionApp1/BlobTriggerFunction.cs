using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionApp1;

public class BlobTriggerFunction
{
    private readonly ILogger<BlobTriggerFunction> _logger;

    public BlobTriggerFunction(ILogger<BlobTriggerFunction> logger)
    {
        _logger = logger;
    }

    [Function(nameof(BlobTriggerFunction))]
    public async Task Run([BlobTrigger("items/{name}", Source = BlobTriggerSource.LogsAndContainerScan, Connection = "blobs")] Stream stream, string name)
    {
        using var blobStreamReader = new StreamReader(stream);
        var content = await blobStreamReader.ReadToEndAsync();
        _logger.LogInformation($"C# Blob Trigger (using Event Grid) processed blob\n Name: {name} \n Data: {content}");
    }
}
