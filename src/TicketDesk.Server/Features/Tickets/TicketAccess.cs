using TicketDesk.Server.Features.Users;
using TicketDesk.Shared.Users;
namespace TicketDesk.Server.Features.Tickets;

public static class TicketAccess
{
    public static IQueryable<Ticket> VisibleTo(this IQueryable<Ticket> q, AppUser U)
    {
        if (!u.IActive) return q.Where(_ => false);

        return U.Role switch
        {
            UserRole.Admin => q,
            UserRole.Agent => q.Where(t =>
                t.RequesterId == U.Id ||
                t.AssigneeId == U.Id ||
                (U.TeaamId != null && t.TeamId == U.TeamId)),
            => q.Where(t => t.RequesterId == U.Id),
            _
        };
    }
}