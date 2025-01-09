var builder = DistributedApplication.CreateBuilder(args);

var eventHubs = builder.AddAzureEventHubs("eventhubs").AddEventHub("items");

var serviceBus = builder.AddAzureServiceBus("servicebus").AddTopic("mytopic", ["mysubscription"]);

var storage = builder.AddAzureStorage("storage");
var blobs = storage.AddBlobs("blobs");
var queues = storage.AddQueues("queues");
var tables = storage.AddTables("tables");

var secrets = builder.AddAzureKeyVault("secrets");

var openai = builder.AddAzureOpenAI("openai");

var cosmosdb = builder.AddAzureCosmosDB("cosmos").AddDatabase("mydb");

var search = builder.AddAzureSearch("search");

var webPubSub = builder.AddAzureWebPubSub("wps");

var logAnalytics = builder.AddAzureLogAnalyticsWorkspace("log");

var annInsights = builder.AddAzureApplicationInsights("appinsights");

var sql = builder.AddAzureSqlServer("sql").AddDatabase("sqldb");

var redis = builder.AddAzureRedis("redis");

var appConfig = builder.AddAzureAppConfiguration("appconfig");

var postgresdb = builder.AddAzurePostgresFlexibleServer("pg").AddDatabase("postgresdb");

var signalR = builder.AddAzureSignalR("signalr");

// https://learn.microsoft.com/ja-jp/dotnet/aspire/serverless/functions?tabs=dotnet-cli&pivots=visual-studio#add-azure-functions-resource
builder.AddAzureFunctionsProject<Projects.FunctionApp1>("functionapp1")
    .WithExternalHttpEndpoints()
    .WithReference(eventHubs)
    .WithReference(serviceBus)
    .WithReference(blobs)
    .WithReference(queues)
    .WithReference(tables)
    .WithReference(secrets)
    .WithReference(openai)
    .WithReference(cosmosdb)
    .WithReference(search)
    .WithReference(webPubSub)
    //.WithReference(logAnalytics)
    .WithReference(annInsights)
    .WithReference(sql)
    .WithReference(redis)
    .WithReference(appConfig)
    .WithReference(postgresdb)
    .WithReference(signalR)
    ;

builder.AddAzureFunctionsProject<Projects.DurableFunctionApp1>("durablefunctionapp1");

builder.Build().Run();
