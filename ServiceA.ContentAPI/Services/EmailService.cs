using ServiceA.ContentApI.DTOs.Requests;
using ServiceA.ContentApI.DTOs.Responses;
using ServiceA.ContentApI.Models;
using ServiceA.ContentApI.Repositorries;
using System.Linq;

namespace ServiceA.ContentApI.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailRepository _repository;

        public EmailService(IEmailRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EmailResponseDto>> GetAllAsync()
        {
            var emails = await _repository.GetAllAsync();

            return emails.Select(e => new EmailResponseDto
            {
                Id = e.Id,
                Subject = e.Subject,
                Tone = e.Tone,
                Content = e.Content
            });
        }

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

        public async Task UpdateAsync(int id, UpdateEmailRequestDto dto)
        {
            var email = await _repository.GetByIdAsync(id);
            if (email == null) return;

            email.Subject = dto.Subject;
            email.Tone = dto.Tone;
            email.Content = dto.Content;

            await _repository.UpdateAsync(email);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }

}

