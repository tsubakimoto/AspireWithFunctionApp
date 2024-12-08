var builder = DistributedApplication.CreateBuilder(args);

var eventHubs = builder.AddAzureEventHubs("eventhubs").AddEventHub("items");

var serviceBus = builder.AddAzureServiceBus("servicebus").AddTopic("mytopic", ["mysubscription"]);

var storage = builder.AddAzureStorage("storage");
var blobs = storage.AddBlobs("blobs");
var queues = storage.AddQueues("queues");
var tables = storage.AddTables("tables");

// https://learn.microsoft.com/ja-jp/dotnet/aspire/serverless/functions?tabs=dotnet-cli&pivots=visual-studio#add-azure-functions-resource
builder.AddAzureFunctionsProject<Projects.FunctionApp1>("functionapp1")
    .WithExternalHttpEndpoints()
    .WithReference(eventHubs)
    .WithReference(serviceBus)
    .WithReference(blobs)
    .WithReference(queues)
    .WithReference(tables)
    ;

builder.Build().Run();
