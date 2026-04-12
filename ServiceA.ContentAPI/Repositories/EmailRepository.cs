using Microsoft.EntityFrameworkCore;
using ServiceA.ContentApi.Data;
using ServiceA.ContentApI.Models;


namespace ServiceA.ContentApI.Repositorries
{
    public class EmailRepository : IEmailRepository
    {
        private readonly AppDbContext _context;

        public EmailRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmailRequest>> GetAllAsync(string? subject)
        {
            var query = _context.EmailRequests.AsQueryable();

            if (!string.IsNullOrEmpty(subject))
            {
                query = query.Where(e => e.Subject.Contains(subject));
            }

            return await query.ToListAsync();
        }


        public async Task<EmailRequest?> GetByIdAsync(int id)
        {
            return await _context.EmailRequests.FindAsync(id);
        }

        public async Task<EmailRequest> CreateAsync(EmailRequest email)
        {
            _context.EmailRequests.Add(email);
            await _context.SaveChangesAsync();
            return email;
        }

        public async Task UpdateAsync(EmailRequest email)
        {
            _context.EmailRequests.Update(email);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var email = await _context.EmailRequests.FindAsync(id);
            if (email != null)
            {
                _context.EmailRequests.Remove(email);
                await _context.SaveChangesAsync();
            }
        }
    }
}

