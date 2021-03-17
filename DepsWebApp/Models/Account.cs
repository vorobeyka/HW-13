using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepsWebApp.Models
{
    /// <summary>
    /// Account model.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Base64 id.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Account login.
        /// </summary>
        public string Login { get; }

        /// <summary>
        /// Account password.
        /// </summary>
        public string Password { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Set <see cref="Id"/>.</param>
        /// <param name="login">Set <see cref="Login"/>.</param>
        /// <param name="password">Set <see cref="Password"/>.</param>
        public Account(string id, string login, string password)
        {
            Id = id;
            Login = login;
            Password = password;
        }
    }
}
