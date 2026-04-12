using ServiceA.ContentApI.DTOs.Requests;
using ServiceA.ContentApI.DTOs.Responses;

namespace ServiceA.ContentApI.Services
{
    public interface IEmailService
    {
        Task<IEnumerable<EmailResponseDto>> GetAllAsync(string? subject);
        Task<EmailResponseDto?> GetByIdAsync(int id);
        Task<EmailResponseDto> CreateAsync(CreateEmailRequestDto dto);
        Task<bool> UpdateAsync(int id, UpdateEmailRequestDto dto);
        Task<bool> DeleteAsync(int id);
        Task<string> SendToServiceBAsync(CreateEmailRequestDto dto);
    }
}
