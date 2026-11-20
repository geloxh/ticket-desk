using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using TicketDesk.Server.Features.Users;
using TicketDesk.Server.Infrastructure.Persistence;
using TicketDesk.Shared.Users;
namespace TicketDesk.Server.Infrastructure.Auth;

public interface ICurrentUser { Task<AppUser> GetAsync(CancellationToken ct); }

public class CurrentUser(IHttpContextAccessor http, AppDbContext db) : ICurrentUser
{
    private AppUser? _cached;

    public async Task<AppUser> GetAsync(CancellationToken ct)
    {
        if (_cached is not null) return _cached;

        var p = http.HttpContext!.User;
        var old = p.FindFirstValue("old")
                   ?? p.FindFirstValue("http://schemas.microsoft.com/identity/claims/objectidentifier")
                   ?? throw new UnauthorizedAccessException("Missing identity claim.");
        
        var user = await db.Users.SingleOrDefaultAsync(u => u.ExternalId == old, ct);
        if (user is null)
        {
            // First login: least-privileged role. Admins promote users manually.
            user = new AppUser
            {
                ExternalId = old,
                DisplayName = p.FindFirstValue("name") ?? "Unknown",
                Email = p.FindFirstValue("preferred_username") ?? "",
                Role = shared.Users.UserRole.Requester,
            };
            db.Users.Add(user);
            await db.SaveChangesAsync(ct);
        }
        return _cached = user; 
    }
}