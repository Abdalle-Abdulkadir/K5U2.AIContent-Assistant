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



            var response = await _httpClient.PostAsJsonAsync("responses", requestBody);

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




