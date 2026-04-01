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
                To = e.To,
                Subject = e.Subject,
                Body = e.Body
            });
        }

        public async Task<EmailResponseDto?> GetByIdAsync(int id)
        {
            var email = await _repository.GetByIdAsync(id);

            if (email == null) return null;

            return new EmailResponseDto
            {
                Id = email.Id,
                To = email.To,
                Subject = email.Subject,
                Body = email.Body
            };
        }

        public async Task<EmailResponseDto> CreateAsync(CreateEmailRequestDto dto)
        {
            var email = new EmailRequest
            {
                To = dto.To,
                Subject = dto.Subject,
                Body = dto.Body
            };

            var created = await _repository.CreateAsync(email);

            return new EmailResponseDto
            {
                Id = created.Id,
                To = created.To,
                Subject = created.Subject,
                Body = created.Body
            };
        }

        public async Task UpdateAsync(int id, UpdateEmailRequestDto dto)
        {
            var email = await _repository.GetByIdAsync(id);
            if (email == null) return;

            email.To = dto.To;
            email.Subject = dto.Subject;
            email.Body = dto.Body;

            await _repository.UpdateAsync(email);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }

}

