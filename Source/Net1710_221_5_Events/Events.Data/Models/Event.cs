using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Events.Data.Models;

public partial class Event
{
    public int EventId { get; set; }

    [Required(ErrorMessage = "Event name is require")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Location is require")]
    public string Location { get; set; } = null!;

    public string? Description { get; set; }

    public string ImageLink { get; set; } = null!;

    [Required(ErrorMessage = "Start date is require")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "End date is require")]
    public DateTime EndDate { get; set; }

    [Required(ErrorMessage = "Open ticket date is require")]
    public DateTime OpenTicket { get; set; }

    [Required(ErrorMessage = "Close ticket date is require")]
    public DateTime CloseTicket { get; set; }

    [Required(ErrorMessage = "Ticket price is require")]
    public int TicketPrice { get; set; }

    [Required(ErrorMessage = "Ticket quantity is require")]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "Operator name is require")]
    public string OperatorName { get; set; } = null!;

    public bool IsDelete { get; set; } = false;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
