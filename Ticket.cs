using System.ComponentModel.DataAnnotations;

namespace CloudDesk.Models;

public enum TicketStatus
{
    Open,
    InProgress,
    Closed
}

public enum TicketPriority
{
    Low,
    Medium,
    High
}

/// <summary>
/// Represents a single IT support ticket.
/// </summary>
public class Ticket
{
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Description { get; set; }

    public TicketStatus Status { get; set; } = TicketStatus.Open;

    public TicketPriority Priority { get; set; } = TicketPriority.Medium;

    [MaxLength(100)]
    public string? AssignedTo { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// URL of an attached screenshot/file in Blob Storage.
    /// Left null for now - we'll populate this on Day 5.
    /// </summary>
    public string? AttachmentUrl { get; set; }
}
