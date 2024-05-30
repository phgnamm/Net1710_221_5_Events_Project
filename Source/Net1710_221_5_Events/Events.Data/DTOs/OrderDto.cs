using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Data.DTOs
{
    public class OrderDto
    {
        public string Code { get; set; } = null!;

        public string? Description { get; set; }

        public int TicketQuantity { get; set; }

        public int TotalAmount { get; set; }

        public string PaymentStatus { get; set; } = null!;

        public DateTime PaymentDate { get; set; }

        public string PaymentMethod { get; set; } = null!;

        public string Status { get; set; } = null!;
    }
}
