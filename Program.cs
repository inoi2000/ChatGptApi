using ChatGptApi;
using System.Net.Http.Headers;
using System.Net.Http.Json;

ChatGpt chatGpt = new ChatGpt(Environment.GetEnvironmentVariable("OPENAI_API_KEY"));
do
{
    var question = Console.ReadLine();
    if(!string.IsNullOrEmpty(question)) Console.WriteLine(await chatGpt.NextQuestion(question));
} while (true);




