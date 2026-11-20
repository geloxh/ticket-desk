using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
namespace TicketDesk.Server.Infrastructure.Auth;

// DEVELOPMENT ONLY: trusts an "X-Dev-User" header. Never register this outside Development.
public class DevAuthHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger, UrlEncoder encoder
) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var name = Request.Headers["X-Dev-User"].ToString().Trim();
        if (name.Length == 0) return Task.FromResult(AuthenticateResult.NoResult());

        var identity = new ClaimsIdentity([
            new Claim("old", name),
            new Claim("name", name),
            new Claim("preferred_username", $"{name}@dev.local"),
        ], Scheme.Name);

        var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), Scheme.Name);
        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}