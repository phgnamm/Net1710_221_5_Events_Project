using System;
using System.Collections.Generic;

namespace Events.Data.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public bool Gender { get; set; }

    public DateTime DateOfBirth { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
