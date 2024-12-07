using Azure.Messaging.EventHubs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionApp1;

public class EventHubsTriggerFunction
{
    private readonly ILogger<EventHubsTriggerFunction> _logger;

    public EventHubsTriggerFunction(ILogger<EventHubsTriggerFunction> logger)
    {
        _logger = logger;
    }

    [Function(nameof(EventHubsTriggerFunction))]
    public void Run([EventHubTrigger("items", Connection = "eventhubs")] EventData[] events)
    {
        foreach (EventData @event in events)
        {
            _logger.LogInformation("Event Body: {body}", @event.Body);
            _logger.LogInformation("Event Content-Type: {contentType}", @event.ContentType);
        }
    }
}
