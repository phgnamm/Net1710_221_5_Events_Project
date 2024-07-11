﻿using System;
using System.Collections.Generic;

namespace Events.Data.Models;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int OrderId { get; set; }

    public int EventId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
