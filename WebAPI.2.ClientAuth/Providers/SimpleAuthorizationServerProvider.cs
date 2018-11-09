using Microsoft.Owin.Security.OAuth;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace WebAPI._2.ClientAuth.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.TryGetFormCredentials(out string id, out string secret))
            {
                if (id == "secret" && secret == "secret")
                {
                    context.Validated(id);
                }
                else { context.Rejected(); }
            }
        }

        public override async Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            var identity = new ClaimsIdentity(new GenericIdentity(
                    context.ClientId, OAuthDefaults.AuthenticationType),
                context.Scope.Select(x => new Claim("urn:oauth:scope", x))
            );
            context.Validated(identity);
        }

        //public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        //{
        //	var id = new ClaimsIdentity(context.Options.AuthenticationType);
        //	id.AddClaim(new Claim("sub", context.UserName));
        //	id.AddClaim(new Claim("role", "user"));

        //	// create metadata to pass on to refresh token provider
        //	var props = new AuthenticationProperties(new Dictionary<string, string>
        //	{
        //		{ "as:client_id", context.ClientId }
        //	});

        //	var ticket = new AuthenticationTicket(id, props);
        //	context.Validated(ticket);
        //	;
        //}
    }
}