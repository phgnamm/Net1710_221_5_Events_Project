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
    public class CreateModel : PageModel
    {
        private readonly IOrderBusiness _orderBusiness;

        public CreateModel(IOrderBusiness orderBusiness)
        {
            _orderBusiness = orderBusiness;
        }

        [BindProperty]
        public Order Order { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await _orderBusiness.CreateNewOrder(Order);
            if (result.Status == Const.SUCCESS_CREATE_CODE)
            {
                return RedirectToPage("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the order.");
                return Page();
            }
        }
    }
}
