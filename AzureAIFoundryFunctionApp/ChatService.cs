using Microsoft.Extensions.AI;

namespace AzureAIFoundryFunctionApp;

public class ChatService(IChatClient chatClient)
{
    public async Task<string> GetChatResponseAsync(string prompt)
    {
        ChatMessage message = new(ChatRole.User, prompt);
        var response = await chatClient.GetResponseAsync(message);
        return response?.Messages.FirstOrDefault()?.Text ?? string.Empty;
    }
}
