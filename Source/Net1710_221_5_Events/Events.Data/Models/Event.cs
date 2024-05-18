using System;
using System.Collections.Generic;

namespace Events.Data.Entities;

public partial class Event
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string ImageLink { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public DateTime OpenTicket { get; set; }

    public DateTime CloseTicket { get; set; }

    public decimal TicketPrice { get; set; }

    public int Quantity { get; set; }

    public Guid OperatorId { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public Guid UpdatedBy { get; set; }

    public virtual Customer Operator { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
