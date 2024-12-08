using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionApp1;

public class QueueTriggerFunction
{
    private readonly ILogger<QueueTriggerFunction> _logger;

    public QueueTriggerFunction(ILogger<QueueTriggerFunction> logger)
    {
        _logger = logger;
    }

    [Function(nameof(QueueTriggerFunction))]
    public void Run([QueueTrigger("items", Connection = "queues")] QueueMessage message)
    {
        _logger.LogInformation($"C# Queue trigger function processed: {message.MessageText}");
    }
}
