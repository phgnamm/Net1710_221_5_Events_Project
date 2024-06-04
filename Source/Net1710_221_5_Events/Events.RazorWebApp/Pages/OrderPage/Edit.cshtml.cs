using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Events.Data.Models;
using Events.Business;
using Events.Common;

namespace Events.RazorWebApp.Pages.OrderPage
{
    public class EditModel : PageModel
    {
        private readonly IOrderBusiness _orderBusiness;

        public EditModel(IOrderBusiness orderBusiness)
        {
            _orderBusiness = orderBusiness;
        }

        [BindProperty]
        public Order Order { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var result = await _orderBusiness.GetOrderById(id);
            if (result.Status == Const.SUCCESS_READ_CODE)
            {
                Order = (Order)result.Data;
                if (Order == null)
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await _orderBusiness.UpdateOrderById(Order);
            if (result.Status == Const.SUCCESS_UPDATE_CODE)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while updating the order.");
                return Page();
            }
        }
    }
}
