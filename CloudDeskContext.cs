using CloudDesk.Models;
using Microsoft.EntityFrameworkCore;

namespace CloudDesk.Data;

public class CloudDeskContext : DbContext
{
    public CloudDeskContext(DbContextOptions<CloudDeskContext> options) : base(options)
    {
    }

    public DbSet<Ticket> Tickets => Set<Ticket>();
}
