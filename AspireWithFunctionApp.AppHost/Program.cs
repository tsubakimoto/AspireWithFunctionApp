var builder = DistributedApplication.CreateBuilder(args);

var eventHubs = builder.AddAzureEventHubs("eventhubs");

// https://learn.microsoft.com/ja-jp/dotnet/aspire/serverless/functions?tabs=dotnet-cli&pivots=visual-studio#add-azure-functions-resource
builder.AddAzureFunctionsProject<Projects.FunctionApp1>("functionapp1")
    .WithExternalHttpEndpoints()
    .WithReference(eventHubs);

builder.Build().Run();
