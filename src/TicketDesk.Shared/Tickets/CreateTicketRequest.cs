namespace TicketDesk.Shared.Tickets;

public record CreateTicketRequest(string Title, string Description, TicketPriority Priority);