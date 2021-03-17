using DepsWebApp.Contexts;
using DepsWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DepsWebApp.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly AccountManagerContext _dbContext;

        public DatabaseService(AccountManagerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAccountAsync(Account account, CancellationToken cancellationToken)
        {
            await _dbContext.Accounts.AddAsync(account, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Account> GetAccountAsync(string login, string password, CancellationToken cancellationToken)
        {
            var account = await _dbContext.Accounts
                .FirstOrDefaultAsync(account => account.Login == login && account.Password == password, cancellationToken);

            return account;
        }
    }
}
