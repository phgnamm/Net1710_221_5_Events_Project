using System;
using System.Collections.Generic;

namespace Events.Data.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public string Code { get; set; } = null!;

    public string? Description { get; set; }

    public int TicketQuantity { get; set; }

    public int TotalAmount { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public DateTime PaymentDate { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
