using DepsWebApp.Converters;
using DepsWebApp.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DepsWebApp.Services
{
    /// <summary>
    /// Implements <see cref="IAccountService"/>.
    /// </summary>
    public class AccountServiceInMemory : IAccountService
    {
        private readonly List<Account> _accounts = new List<Account>();
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        /// <inheritdoc/>
        public async Task<string> RegisterAsync(string login, string password)
        {
            if (login == null || password == null) throw new ArgumentNullException();
            if (_accounts.Any(account => account.Login == login)) return null;

            var id = BaseConverter.ToBase64String(login + ":" + password);

            await _semaphore.WaitAsync();
            _accounts.Add(new Account(id, login, password));
            _semaphore.Release();

            return id;
        }

        /// <inheritdoc/>
        public async Task<string> FindAsync(string encodedAccount)
        {
            if (encodedAccount == null) throw new ArgumentNullException(nameof(encodedAccount));

            var login = encodedAccount.Split(':')[0];
            var password = encodedAccount.Split(':')[1];

            await _semaphore.WaitAsync();
            var account = _accounts.FirstOrDefault(acc => acc.Login == login && acc.Password == password);
            _semaphore.Release();

            return account.Id;
        }
    }
}
