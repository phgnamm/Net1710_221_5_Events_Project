using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Events.Data.Models;
using Events.Business;
using Events.Common;

namespace Events.RazorWebApp.Pages.OrderPage
{
    public class IndexModel : PageModel
    {
        private readonly IOrderBusiness _orderBusiness;

        public IndexModel(IOrderBusiness orderBusiness)
        {
            _orderBusiness = orderBusiness;
        }

        public IList<Order> Order { get; set; } = new List<Order>();

        public async Task OnGetAsync()
        {
            var result = await _orderBusiness.GetAllOrders();
            if (result.Status == Const.SUCCESS_READ_CODE)
            {
                Order = (IList<Order>)result.Data;
            }
            else
            {
                Order = new List<Order> ();
            }
        }
    }
}