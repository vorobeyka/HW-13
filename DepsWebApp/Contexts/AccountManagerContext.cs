using DepsWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DepsWebApp.Contexts
{
    public class AccountManagerContext : DbContext
    {
        public AccountManagerContext(DbContextOptions<AccountManagerContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=qwerty");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .ToTable("Accounts");
        }
    }
}
