using TicketDesk.shared.Users;
using TicketDesk.Shared.Users;
namespace TicketDesk.Server.Features.Users;

public class AppUser
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ExternalId { get; set; } = ""; // login indetity (Entra "old| later)
    public string DisplayName { get; set; } = "";
    public string Email { get; set; } = "";
    public UserRole Role { get; set; } = UserRole.Requester;
    public Guid? TeamId { get; set; }
    public bool isActive { get; set; } = true;
}