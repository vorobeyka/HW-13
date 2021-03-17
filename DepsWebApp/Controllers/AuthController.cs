using System.Threading;
using System.Threading.Tasks;
using DepsWebApp.Models;
using DepsWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DepsWebApp.Controllers
{
    /// <summary>
    /// Authorization controller for register accounts.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(400)]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _accountService;

        /// <summary>
        /// AuthController constructor
        /// </summary>
        /// <param name="accountService">Set <see cref="_accountService"/></param>
        public AuthController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Register account in memory.
        /// </summary>
        /// <param name="request">Account</param>
        /// <returns>Base64 string.</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<ActionResult<string>> Register([FromBody]AuthRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var account = new Account { Login = request.Login, Password = request.Password };
            var baseKey = await _accountService.RegisterAsync(account, cancellationToken);
            if (string.IsNullOrEmpty(baseKey)) return BadRequest("User alredy exists");
            return baseKey;
        }
    }
}
