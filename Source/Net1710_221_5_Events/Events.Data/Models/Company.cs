using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Events.Data.Models;

public partial class Company 
{
    public int CompanyId { get; set; }
    public string Name { get; set; }
    public string CompanyPhone { get; set; }
    public string BusinessSector { get; set; }
    public string TaxesId { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
