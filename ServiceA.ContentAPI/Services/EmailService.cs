using ServiceA.ContentApi.Clients;
using ServiceA.ContentApI.DTOs.Requests;
using ServiceA.ContentApI.DTOs.Responses;
using ServiceA.ContentApI.Models;
using ServiceA.ContentApI.Repositorries;
using System.Linq;


namespace ServiceA.ContentApI.Services
{
    // Service som hanterar logiken för email requests
    public class EmailService : IEmailService
    {
        private readonly IEmailRepository _repository;
        private readonly LlmProxyClient _llmClient;

        public EmailService(IEmailRepository repository, LlmProxyClient llmClient)
        {
            _repository = repository;
            _llmClient = llmClient;
        }
        // Metod för att hämta alla email requests
        public async Task<IEnumerable<EmailResponseDto>> GetAllAsync(string? subject)
        {
            var emails = await _repository.GetAllAsync(subject);

            return emails.Select(e => new EmailResponseDto
            {
                Id = e.Id,
                Subject = e.Subject,
                Tone = e.Tone,
                Content = e.Content
            });
        }
        // Metod för att hämta en email request baserat på id
        public async Task<EmailResponseDto?> GetByIdAsync(int id)
        {
            var email = await _repository.GetByIdAsync(id);

            if (email == null) return null;

            return new EmailResponseDto
            {
                Id = email.Id,
                Tone = email.Tone,
                Subject = email.Subject,
                Content = email.Content
            };
        }
        // Metod för att skapa en email request
        public async Task<EmailResponseDto> CreateAsync(CreateEmailRequestDto dto)
        {
            var email = new EmailRequest
            {
                Subject = dto.Subject,
                Tone = dto.Tone,
                Content = dto.Content
            };

            var created = await _repository.CreateAsync(email);

            return new EmailResponseDto
            {
                Id = created.Id,
                Subject = created.Subject,
                Tone = created.Tone,
                Content = created.Content
            };
        }
        // Metod för att uppdatera en email request
        public async Task<bool> UpdateAsync(int id, UpdateEmailRequestDto dto)
        {
            var email = await _repository.GetByIdAsync(id);

            if (email == null) return false;

            email.Subject = dto.Subject;
            email.Tone = dto.Tone;
            email.Content = dto.Content;

            await _repository.UpdateAsync(email);

            return true;
        }



        // Metod för att radera en email request
        public async Task<bool> DeleteAsync(int id)
        {
            var email = await _repository.GetByIdAsync(id);

            if (email == null) return false;

            await _repository.DeleteAsync(id);

            return true;
        }


        // Metod för att skicka data till Service B
        public async Task<string> SendToServiceBAsync(CreateEmailRequestDto dto)
        {
            var response = await _llmClient.GenerateEmailAsync(dto);
            return response;

        }
    }
}