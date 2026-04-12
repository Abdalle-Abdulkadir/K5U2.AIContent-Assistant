using Microsoft.EntityFrameworkCore;
using ServiceA.ContentApI.Models;

namespace ServiceA.ContentApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<EmailRequest> EmailRequests { get; set; }
    }
}


