using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Events.Data.Models;
using Events.Business;
using Events.Common;

namespace Events.RazorWebApp.Pages.OrderPage
{
    public class DetailsModel : PageModel
    {
        private readonly IOrderBusiness _orderBusiness;

        public DetailsModel(IOrderBusiness orderBusiness)
        {
            _orderBusiness = orderBusiness;
        }

        public Order Order { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

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
    }
}