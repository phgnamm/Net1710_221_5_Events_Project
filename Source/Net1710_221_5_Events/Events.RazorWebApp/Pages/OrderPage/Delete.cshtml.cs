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
    public class DeleteModel : PageModel
    {
        private readonly IOrderBusiness _orderBusiness;

        public DeleteModel(IOrderBusiness orderBusiness)
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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var result = await _orderBusiness.DeleteOrderById(id);
            if (result.Status == Const.SUCCESS_DELETE_CODE)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the order.");
                return Page();
            }
        }
    }
}
