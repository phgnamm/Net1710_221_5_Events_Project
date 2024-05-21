using System;
using System.Collections.Generic;

namespace Events.Data.Models;

public partial class Company
{
    public int CompanyId { get; set; }

    public string Name { get; set; } = null!;

    public string BusinessSector { get; set; } = null!;

    public string TaxesId { get; set; } = null!;

    public string Address { get; set; } = null!;
}
