using System.Net.Http.Json;
using ServiceA.ContentApI.DTOs.Requests;

namespace ServiceA.ContentApi.Clients
{
    public class LlmProxyClient
    {
        private readonly HttpClient _httpClient;

        public LlmProxyClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GenerateEmailAsync(CreateEmailRequestDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/LLM", dto);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}



