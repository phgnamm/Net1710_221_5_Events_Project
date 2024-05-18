using System;
using System.Collections.Generic;

namespace Events.Data.Entities;

public partial class Ticket
{
    public Guid Id { get; set; }

    public DateTime Date { get; set; }

    public string Qrcode { get; set; } = null!;

    public string Description { get; set; } = null!;

    public Guid OrderDetailId { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public Guid UpdatedBy { get; set; }

    public virtual OrderDetail OrderDetail { get; set; } = null!;
}
