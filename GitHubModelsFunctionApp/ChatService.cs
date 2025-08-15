using OpenAI;
using OpenAI.Chat;

namespace GitHubModelsFunctionApp;

public class ChatService(OpenAIClient client)
{
    public async Task<string> GetChatResponseAsync(string prompt)
    {
        var chatClient = client.GetChatClient("openai/gpt-4o-mini");

        var response = await chatClient.CompleteChatAsync(
            new[]
            {
                new UserChatMessage(prompt)
            });

        return response.Value.Content[0].Text;
    }
}