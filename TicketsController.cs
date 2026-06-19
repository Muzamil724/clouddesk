using CloudDesk.Data;
using CloudDesk.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CloudDesk.Controllers;

[ApiController]
[Route("api/tickets")]
public class TicketsController : ControllerBase
{
    private readonly CloudDeskContext _context;

    public TicketsController(CloudDeskContext context)
    {
        _context = context;
    }

    /// <summary>
    /// GET /api/tickets
    /// GET /api/tickets?status=Open
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets([FromQuery] TicketStatus? status)
    {
        var query = _context.Tickets.AsQueryable();

        if (status is not null)
        {
            query = query.Where(t => t.Status == status);
        }

        return await query.OrderByDescending(t => t.CreatedAt).ToListAsync();
    }

    /// <summary>
    /// GET /api/tickets/5
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Ticket>> GetTicket(int id)
    {
        var ticket = await _context.Tickets.FindAsync(id);

        if (ticket is null)
        {
            return NotFound();
        }

        return ticket;
    }

    /// <summary>
    /// POST /api/tickets
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Ticket>> CreateTicket(Ticket ticket)
    {
        ticket.Id = 0;
        ticket.CreatedAt = DateTime.UtcNow;

        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTicket), new { id = ticket.Id }, ticket);
    }

    /// <summary>
    /// PUT /api/tickets/5
    /// Updates title, description, status, priority and assignee.
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTicket(int id, Ticket updated)
    {
        var ticket = await _context.Tickets.FindAsync(id);

        if (ticket is null)
        {
            return NotFound();
        }

        ticket.Title = updated.Title;
        ticket.Description = updated.Description;
        ticket.Status = updated.Status;
        ticket.Priority = updated.Priority;
        ticket.AssignedTo = updated.AssignedTo;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// DELETE /api/tickets/5
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTicket(int id)
    {
        var ticket = await _context.Tickets.FindAsync(id);

        if (ticket is null)
        {
            return NotFound();
        }

        _context.Tickets.Remove(ticket);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
