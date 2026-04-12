using System.Net.Http.Json;
using ServiceB.LLMProxyAPI.DTOs;

namespace ServiceB.LLMProxyAPI.Services
{
    public class LlmService
    {
        private readonly HttpClient _httpClient;

        public LlmService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
                model = "llama3",
                prompt = prompt,
                stream = false,
                options = new
                {
                    num_predict = 150
                }
            };

            var response = await _httpClient.PostAsJsonAsync("/api/generate", requestBody);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return $"Error: {error}";
            }

            var json = await response.Content.ReadAsStringAsync();

            using var doc = System.Text.Json.JsonDocument.Parse(json);
            var result = doc.RootElement.GetProperty("response").GetString();

            return result ?? "No response";
        }
    }
}




