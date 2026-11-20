namespace TicketDesk.Server.Features.Audit;

public class Auditing
{
    public long id { get; set }
    public Guid ActorId { get; set; }
    public string Action { get; set; } = "";
    public string EntityType { get; set; } = "";
    public Guid EntityId { get; set; }
    public string? Details { get; set; } // IDs and field names only, never secrets
    public DateTime At { get; set; } = DateTime.UtcNow;
}