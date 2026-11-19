namespace TicketDesk.Shared.Tickets;

public record TicketDto(
    Guid Id, string Title, string Description,
    TicketStatus Status, TicketPriority Priority,
    Guid RequesterId, Guid? AssignedId,
    DateTime CreatedAt, DateTime? DueAt
);