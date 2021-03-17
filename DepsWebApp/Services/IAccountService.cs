using DepsWebApp.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DepsWebApp.Services
{
    /// <summary>
    /// Account service abstraction.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Registers account and assigns base64 string.
        /// </summary>
        /// <param name="account">Account.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns id of the created account or <c>null</c> if login already existed.</returns>
        /// <exception cref="ArgumentNullException">Throws when one of the arguments is null.</exception>
        Task<string> RegisterAsync(Account account, CancellationToken cancellationToken);

        /// <summary>
        /// Find account and returns its id.
        /// </summary>
        /// <param name="encodedAccount">Account encoded login and password.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns account or <c>null</c> if user wasn't found or password is invalid.</returns>
        /// <exception cref="ArgumentNullException">Throws when argument is null.</exception>
        Task<Account> FindAsync(string encodedAccount, CancellationToken cancellationToken);
    }
}
