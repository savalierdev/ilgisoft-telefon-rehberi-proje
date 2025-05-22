using Microsoft.EntityFrameworkCore;
using TelefonRehberi.Models;

namespace TelefonRehberi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Kisi> Kisiler { get; set; } = null!;
    }
}