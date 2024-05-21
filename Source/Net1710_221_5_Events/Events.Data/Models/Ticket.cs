using System;
using System.Collections.Generic;

namespace Events.Data.Models;

public partial class Ticket
{
    public int TicketId { get; set; }

    public string Code { get; set; } = null!;

    public string Qrcode { get; set; } = null!;

    public int OrderDetailId { get; set; }

    public DateTime CreatedDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual OrderDetail OrderDetail { get; set; } = null!;
}
