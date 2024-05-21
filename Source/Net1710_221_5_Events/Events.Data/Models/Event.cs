using System;
using System.Collections.Generic;

namespace Events.Data.Models;

public partial class Event
{
    public int EventId { get; set; }

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string? Description { get; set; }

    public string ImageLink { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public DateTime OpenTicket { get; set; }

    public DateTime CloseTicket { get; set; }

    public int TicketPrice { get; set; }

    public int Quantity { get; set; }

    public string OperatorName { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
