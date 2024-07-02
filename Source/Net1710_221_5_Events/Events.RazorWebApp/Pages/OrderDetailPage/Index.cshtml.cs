using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Events.Data.Models;
using Events.Business.Business;

namespace Events.RazorWebApp.Pages.OrderDetailPage
{
    public class IndexModel : PageModel
    {
        private readonly IOrderDetailBusiness business;
        public IndexModel()
        {
            business ??= new OrderDetailBusiness(); 
        }

        public IList<OrderDetail> OrderDetail { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var result = await business.GetAllOrderDetailsAsync();
            if(result != null && result.Status > 0 && result.Data != null)
            {
                OrderDetail = result.Data as List<OrderDetail>;
            }
        }
    }
}
