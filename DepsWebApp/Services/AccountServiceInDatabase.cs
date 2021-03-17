using DepsWebApp.Converters;
using DepsWebApp.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DepsWebApp.Services
{
    /// <summary>
    /// Implements <see cref="IAccountService"/>.
    /// </summary>
    public class AccountServiceInDatabase : IAccountService
    {
        private readonly IDatabaseService _databaseService;

        public AccountServiceInDatabase(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        /// <inheritdoc/>
        public async Task<string> RegisterAsync(Account account, CancellationToken cancellationToken)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));
            if (await _databaseService.GetAccountAsync(account.Login, account.Password, cancellationToken) != null) return null;

            await _databaseService.AddAccountAsync(account, cancellationToken);

            var baseAuth = BaseConverter.ToBase64String(account.Login + ":" + account.Password);

            return baseAuth;
        }

        /// <inheritdoc/>
        public async Task<Account> FindAsync(string encodedAccount, CancellationToken cancellationToken)
        {
            if (encodedAccount == null) throw new ArgumentNullException(nameof(encodedAccount));

            var login = encodedAccount.Split(':')[0];
            var password = encodedAccount.Split(':')[1];
            var account = await _databaseService.GetAccountAsync(login, password, cancellationToken);

            return account;
        }
    }
}
