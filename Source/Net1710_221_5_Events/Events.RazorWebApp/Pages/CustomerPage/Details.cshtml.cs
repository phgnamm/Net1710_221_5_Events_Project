using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Events.Data.Models;
using Events.Business.Business;

namespace Events.RazorWebApp.Pages.CustomerPage
{
    public class DetailsModel : PageModel
    {
        private readonly ICustomerBusiness business;

        public DetailsModel()
        {
            business ??= new CustomerBusiness();
        }

        public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await business.GetCustomer(id.Value);
            if (customer != null && customer.Status > 0 && customer.Data != null)
            {
                Customer = customer.Data as Customer;
                return Page();
            }
            return NotFound();
        }
    }
}
