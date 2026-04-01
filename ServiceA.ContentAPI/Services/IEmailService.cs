using ServiceA.ContentApI.DTOs.Requests;
using ServiceA.ContentApI.DTOs.Responses;

namespace ServiceA.ContentApI.Services
{
    public interface IEmailService
    {
        Task<IEnumerable<EmailResponseDto>> GetAllAsync();
        Task<EmailResponseDto?> GetByIdAsync(int id);
        Task<EmailResponseDto> CreateAsync(CreateEmailRequestDto dto);
        Task UpdateAsync(int id, UpdateEmailRequestDto dto);
        Task DeleteAsync(int id);
    }
}
