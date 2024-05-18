using System;
using System.Collections.Generic;

namespace Events.Data.Entities;

public partial class OrderDetail
{
    public Guid Id { get; set; }

    public string Code { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal Amount { get; set; }

    public decimal Price { get; set; }

    public Guid EventId { get; set; }

    public Guid OrderId { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public Guid UpdatedBy { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
