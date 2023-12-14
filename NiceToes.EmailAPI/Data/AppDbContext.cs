using Microsoft.EntityFrameworkCore;
using NiceToes.EmailAPI.Models;

namespace NiceToes.EmailAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<EmailLogger> EmailLoggers { get; set; }

        
    }
}
