using ServiceA.ContentApI.Models;

namespace ServiceA.ContentApI.Repositorries
{
    public interface IEmailRepository
    {
        Task<IEnumerable<EmailRequest>> GetAllAsync();
        Task<EmailRequest?> GetByIdAsync(int id);
        Task<EmailRequest> CreateAsync(EmailRequest email);
        Task UpdateAsync(EmailRequest email);
        Task DeleteAsync(int id);
    }
}

