using Microsoft.AspNetCore.Authentication;

namespace DepsWebApp.Authentication
{
    public class AuthSchemaOptions : AuthenticationSchemeOptions
    {
        public AuthSchemaOptions()
        {
            ClaimsIssuer = AuthSchema.Issuer;
        }
    }
}
