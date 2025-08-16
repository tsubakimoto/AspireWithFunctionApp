using GitHubModelsFunctionApp;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

// https://learn.microsoft.com/en-us/dotnet/aspire/github/github-models-integration?tabs=dotnet-cli#using-openai-client
builder.AddOpenAIClient("ghModelsChat").AddChatClient();

builder.AddServiceDefaults();

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

builder.Services.AddSingleton<ChatService>();

builder.Build().Run();
