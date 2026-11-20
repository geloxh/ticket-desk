using Microsoft.EntityFrameworkCore.Metadata.Internal;

using TicketDesk.Server.Infrastructure.Persistence;
namespace TicketDesk.Server.Features.Audit;

public class AuditService(AppDbContext db)
{
    // Added to the current unit of work; saved together with the business change.
    public void Record(Guid actorId, string action, string entityType, Guid entityId, string? details = null) => db.AuditLogs.Add(new AuditLog
    {
        actorId = actorId, Action = action,
        EntityType = entityType, EntityId = entityId, Details = details, 
    });
}