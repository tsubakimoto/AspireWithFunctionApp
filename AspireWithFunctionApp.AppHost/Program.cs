var builder = DistributedApplication.CreateBuilder(args);

var eventHubs = builder.AddAzureEventHubs("eventhubs").RunAsEmulator().AddHub("items");

var storage = builder.AddAzureStorage("storage").RunAsEmulator();
var blobs = storage.AddBlobs("blobs");
var queues = storage.AddQueues("queues");
var tables = storage.AddTables("tables");

var cosmosdb = builder.AddAzureCosmosDB("cosmos").RunAsEmulator().AddCosmosDatabase("mydb");

var sql = builder.AddAzureSqlServer("sql").RunAsContainer().AddDatabase("sqldb");

var redis = builder.AddAzureRedis("redis").RunAsContainer();

var postgresdb = builder.AddAzurePostgresFlexibleServer("pg").RunAsContainer().AddDatabase("postgresdb");

var serviceBus = builder.AddAzureServiceBus("servicebus").RunAsEmulator().AddServiceBusTopic("mytopic", "mysubscription");

var secrets = builder.AddAzureKeyVault("secrets");

var openai = builder.AddAzureOpenAI("openai");

var search = builder.AddAzureSearch("search");

var webPubSub = builder.AddAzureWebPubSub("wps");

var logAnalytics = builder.AddAzureLogAnalyticsWorkspace("log");

var appInsights = builder.AddAzureApplicationInsights("appinsights");

var appConfig = builder.AddAzureAppConfiguration("appconfig");

var signalR = builder.AddAzureSignalR("signalr").RunAsEmulator();

// https://learn.microsoft.com/ja-jp/dotnet/aspire/serverless/functions?tabs=dotnet-cli&pivots=visual-studio#add-azure-functions-resource
builder.AddAzureFunctionsProject<Projects.FunctionApp1>("functionapp1")
    .WithHostStorage(storage)
    .WithReference(eventHubs).WaitFor(eventHubs)
    .WithReference(blobs).WaitFor(blobs)
    .WithReference(queues).WaitFor(queues)
    .WithReference(tables).WaitFor(tables)
    .WithReference(cosmosdb).WaitFor(cosmosdb)
    .WithReference(sql).WaitFor(sql)
    .WithReference(redis).WaitFor(redis)
    .WithReference(postgresdb).WaitFor(postgresdb)
    .WithReference(serviceBus).WaitFor(serviceBus)
    .WithReference(secrets).WaitFor(secrets)
    .WithReference(openai).WaitFor(openai)
    .WithReference(search).WaitFor(search)
    .WithReference(webPubSub).WaitFor(webPubSub)
    //.WithReference(logAnalytics).WaitFor(logAnalytics)
    .WithReference(appInsights).WaitFor(appInsights)
    .WithReference(appConfig).WaitFor(appConfig)
    .WithReference(signalR).WaitFor(signalR)
    .WithExternalHttpEndpoints()
    ;

builder.AddAzureFunctionsProject<Projects.DurableFunctionApp1>("durablefunctionapp1");

builder.Build().Run();
