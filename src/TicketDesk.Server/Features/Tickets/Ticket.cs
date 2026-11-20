using TicketDesk.Shared.Tickets;
namespace TicketDesk.Server.Features.Tickets;

public class Ticket
{
    private Ticket() { } // for FF Core

    public static Ticket Create(string title, string description, TicketPriority priority, Guid requesterId) => new()
    {
        Title = title, Description = description,
        Priority = priority, RequesterId = requesterId,
    };

    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Title { get; private set; } = "";
    public string Description { get; private set; } = "";
    public TicketStatus Status{ get; private set; } = TicketStatus.New;
    public TicketPriority Priority { get; private set; }
    public Guid RequesterId { get; private set; }
    public Guid? AssigneeId { get; private set; }
    public Guid? TeamId { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? DueAt { get; private set; }