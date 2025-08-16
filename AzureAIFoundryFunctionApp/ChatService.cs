using Microsoft.Extensions.AI;
//using OpenAI;
//using OpenAI.Chat;

namespace AzureAIFoundryFunctionApp;

//public class ChatService(OpenAIClient client)
//{
//    public async Task<string> GetChatResponseAsync(string prompt)
//    {
//        var chatClient = client.GetChatClient("phi-3.5-mini");

//        var response = await chatClient.CompleteChatAsync(
//            new[]
//            {
//                new UserChatMessage(prompt)
//            });

//        return response.Value.Content[0].Text;
//    }
//}

public class ChatService(IChatClient chatClient)
{
    public async Task<string> GetChatResponseAsync(string prompt)
    {
        ChatMessage message = new(ChatRole.User, prompt);
        var response = await chatClient.GetResponseAsync(message);
        return response?.Messages.FirstOrDefault()?.Text ?? string.Empty;
    }
}
