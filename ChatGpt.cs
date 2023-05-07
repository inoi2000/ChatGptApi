using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ChatGptApi
{
    public class ChatGpt
    {
        private readonly HttpClient _httpClient;
        private string? chatCompletionUrl => "https://api.pawan.krd/v1/chat/completions";

        string question = string.Empty;
        ChatGptRequest request;
        HttpResponseMessage response;
        ChatGptResponse? chatGptResponse;

        public ChatGpt(string token)
        {
            _httpClient = new HttpClient() { DefaultRequestHeaders = { Authorization = AuthenticationHeaderValue.Parse($"Bearer {token}") } };
            
        }

        public async Task<string> NewQuestion(string question)
        {
            this.question = question;
            request = new ChatGptRequest(question);
            response = await _httpClient.PostAsJsonAsync<ChatGptRequest>(chatCompletionUrl, request);
            response.EnsureSuccessStatusCode();
            chatGptResponse = await response.Content.ReadFromJsonAsync<ChatGptResponse>();
            return chatGptResponse.Choices[0].Message.Content;
        }

        public async Task<string> NextQuestion(string question)
        {
            this.question = question;
            if (request != null)
            {
                request.AddMessage(question);
                response = await _httpClient.PostAsJsonAsync<ChatGptRequest>(chatCompletionUrl, request);
                chatGptResponse = await response.Content.ReadFromJsonAsync<ChatGptResponse>();
                return chatGptResponse.Choices[0].Message.Content;
            }
            else { return await NewQuestion(question); }
        }
    }
}
