using AzureAIFoundryFunctionApp;
using Microsoft.AI.Foundry.Local;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenAI;
using System.ClientModel;

var builder = FunctionsApplication.CreateBuilder(args);

// https://learn.microsoft.com/en-us/dotnet/aspire/azureai/azureai-foundry-integration?tabs=dotnet-cli#client-integration
builder.AddAzureChatCompletionsClient(connectionName: "chat").AddChatClient();

builder.AddServiceDefaults();

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

// Foundry Local
if (Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT") == "Development")
{
    var alias = "phi-3.5-mini";
    var manager = await FoundryLocalManager.StartModelAsync(aliasOrModelId: alias);
    var model = await manager.GetModelInfoAsync(aliasOrModelId: alias);
    ApiKeyCredential key = new(manager.ApiKey);
    OpenAIClient client = new(key, new OpenAIClientOptions
    {
        Endpoint = manager.Endpoint
    });
    var chatClient = client.GetChatClient(model?.ModelId).AsIChatClient();
    builder.Services.AddSingleton(chatClient);
}

builder.Services.AddSingleton<ChatService>();

builder.Build().Run();
