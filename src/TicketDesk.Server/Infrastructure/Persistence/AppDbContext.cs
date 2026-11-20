using Microsoft.EntityFrameworkCore;
using TicketDesk.Server.Features.Audit;
using TicketDesk.Server.Features.Tickets;
using TicketDesk.Server.Features.Users;
namespace TicketDesk.Server.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<AppUser> Users => Set<AppUser>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<Auditing> AuditLogs => Set<Auditing>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<AppUser>().HasIndex(u => u.ExternalId).IsUnique();

        b.Entity<Ticket>().Property(t => t.Title).HasMaxLength(200);
        b.Entity<Ticket>().Property(t => t.Description).HasMaxLength(10_000);
        b.Entity<Ticket>().HasIndex(t => new { t.Status, t.AssigneeId });
        b.Entity<Ticket>().HasIndex(t => t.CreatedAt);
    }
}