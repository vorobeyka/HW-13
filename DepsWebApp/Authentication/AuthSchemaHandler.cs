using DepsWebApp.Converters;
using DepsWebApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace DepsWebApp.Authentication
{
    public class AuthSchemaHandler : AuthenticationHandler<AuthSchemaOptions>
    {
        private readonly IAccountService _accountService;

        ///<inheritdoc/>
        public AuthSchemaHandler(
            IOptionsMonitor<AuthSchemaOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IAccountService accountService)
            : base(options, logger, encoder, clock)
        {
            _accountService = accountService;
        }

        ///<inheritdoc/>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!TryGetKeyFromRequest(Request, out var authKey)) return AuthenticateResult.NoResult();
            string accountId;

            try
            {
                var key = BaseConverter.FromBase64String(authKey);
                accountId = await _accountService.FindAsync(key);
                if (accountId == null) throw new Exception();
            }
            catch (Exception)
            {
                return AuthenticateResult.Fail("Invalid login or password");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, accountId),
            };

            var claimsId = new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            return AuthenticateResult.Success(
                new AuthenticationTicket(new ClaimsPrincipal(claimsId), AuthSchema.Name));
        }

        private static bool TryGetKeyFromRequest(HttpRequest request, out string authKey)
        {
            authKey = null;
            if (request.Headers.ContainsKey(HeaderNames.Authorization))
            {
                Console.WriteLine(request.Headers[HeaderNames.Authorization]);
                authKey = request.Headers[HeaderNames.Authorization].FirstOrDefault();
            }
            return !string.IsNullOrEmpty(authKey);
        }
    }
}
