using DepsWebApp.Models;
using System.Threading;
using System.Threading.Tasks;

namespace DepsWebApp.Services
{
    public interface IDatabaseService
    {
        Task AddAccountAsync(Account account, CancellationToken cancellationToken);

        Task<Account> GetAccountAsync(string login, string password, CancellationToken cancellationToken);
    }
}
