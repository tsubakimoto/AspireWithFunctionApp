using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureAIFoundryFunctionApp;

public class Function1(ILogger<Function1> _logger, ChatService _chatService)
{
    [Function("Chat")]
    public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        var prompt = req.Query["prompt"].ToString() ?? string.Empty;
        if (string.IsNullOrEmpty(prompt))
        {
            return new BadRequestObjectResult(new { Error = "Prompt is required." });
        }
        _logger.LogInformation($"Received prompt: {prompt}");

        var chatResponse = await _chatService.GetChatResponseAsync(prompt);
        return new OkObjectResult(chatResponse);
    }
}