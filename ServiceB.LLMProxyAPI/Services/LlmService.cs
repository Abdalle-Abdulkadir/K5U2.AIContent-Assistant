using System.Net.Http.Json;
using ServiceB.LLMProxyAPI.DTOs;

namespace ServiceB.LLMProxyAPI.Services
{
    public class LlmService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public LlmService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> GenerateAsync(string content)
        {
            var prompt = $@"
            Skriv ett mejl enligt dessa regler:

            - Börja alltid med: Hej
            - Skriv endast själva mejlet
            - Inga förklaringar
            - Inga exempel
            - Inga platshållare
            - Själva mejlet ska vara 4 till 5 meningar max
            - Avsluta alltid med: Med vänliga hälsningar

            Uppgift:
            {content}
            ";

            var requestBody = new
            {
                model = "gpt-4.1-mini",
                input = prompt
            };

            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer",
                    _configuration["OpenAI:ApiKey"]);



            HttpResponseMessage response;

            try
            {
                response = await _httpClient.PostAsJsonAsync("responses", requestBody);
            }
            catch (TaskCanceledException)
            {
                return "AI request timed out. Please try again later.";
            }

            // Handle common error scenarios
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                return "AI authentication failed. Check API key or permissions.";
            }

            // Handle rate limiting and server errors
            if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                return "AI service is temporarily busy. Please try again later.";
            }

            // For 5xx errors, we can assume it's an issue on the AI provider's side
            if ((int)response.StatusCode >= 500)
            {
                return "AI service is currently unavailable. Please try again later.";
            }

            // For other non-success status codes, return the error message from the response
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();

                return $"AI request failed. Status: {(int)response.StatusCode}. Message: {error}";
            }

            var json = await response.Content.ReadAsStringAsync();

            string? result;
            try
            {
                using var doc = System.Text.Json.JsonDocument.Parse(json);
                result = doc.RootElement
                    .GetProperty("output")[0]
                    .GetProperty("content")[0]
                    .GetProperty("text")    
                    .GetString();
            }
            catch
            {
                return "AI returned an unexpected response format";
            }

            if (string.IsNullOrWhiteSpace(result))
            {
                return "AI returned an empty response.";
            }
            return result;
        }
    }
}




