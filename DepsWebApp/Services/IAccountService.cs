using DepsWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepsWebApp.Services
{
    /// <summary>
    /// Account service abstraction.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Registers account and assigns unique account id.
        /// </summary>
        /// <param name="login">Account login.</param>
        /// <param name="password">Account password.</param>
        /// <returns>Returns id of the created account or <c>null</c> if login already existed.</returns>
        /// <exception cref="ArgumentNullException">Throws when one of the arguments is null.</exception>
        Task<string> RegisterAsync(string login, string password);

        /// <summary>
        /// Find account and returns its id.
        /// </summary>
        /// <param name="encodedAccount">Account encoded login and password.</param>
        /// <returns>Returns account id or <c>null</c> if user wasn't found or password is invalid.</returns>
        /// <exception cref="ArgumentNullException">Throws when argument is null.</exception>
        Task<string> FindAsync(string encodedAccount);
    }
}
