using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Events.Data.Models;
using Events.Business.Business;

namespace Events.RazorWebApp.Pages.CustomerPage
{
    public class EditModel : PageModel
    {
        private readonly ICustomerBusiness business;

        public EditModel()
        {
            business ??= new CustomerBusiness();
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;
        public List<SelectListItem> GenderList { get; set; }

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
                GenderList = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Male", Text = "Male" },
                    new SelectListItem { Value = "Female", Text = "Female" },
                    new SelectListItem { Value = "Others", Text = "Others" }
                };
                return Page();
            }
            return NotFound();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                GenderList = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Male", Text = "Male" },
                    new SelectListItem { Value = "Female", Text = "Female" },
                    new SelectListItem { Value = "Others", Text = "Others" }
                };

                return Page();
            }

          //  _context.Attach(Customer).State = EntityState.Modified;

            try
            {
                  await business.Update(Customer);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(Customer.CustomerId).Result)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> CustomerExists(int id)
        {
            var result = await business.GetCustomer(id);
            if (result != null && result.Status > 0 && result.Data != null)
            {
                return true;
            }

            return false;
        }
    }
}
