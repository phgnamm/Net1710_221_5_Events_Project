using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Events.Data.Models;
using Events.Business.Business;
using Events.Business;

namespace Events.RazorWebApp.Pages.OrderDetailPage
{
    public class CreateModel : PageModel
    {
        private readonly IOrderDetailBusiness business;
        private readonly IEventBusiness eventBusiness;
        private readonly IOrderBusiness orderBusiness;
        public CreateModel()
        {
            business ??= new OrderDetailBusiness();
            eventBusiness ??= new EventBusiness();
            orderBusiness ??= new OrderBusiness();
        }

        public async Task<IActionResult> OnGet()
        {
            var listEvent = await eventBusiness.GetAllEvents();
            var listOrder = await orderBusiness.GetAllOrders();
            ViewData["EventId"] = new SelectList(listEvent.Data as List<Event>, "EventId", "Name");
            ViewData["OrderId"] = new SelectList(listOrder.Data as List<Order>, "OrderId", "Code");
            return Page();
        }

        [BindProperty]
        public OrderDetail OrderDetail { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
           
            await business.CreateOrderDetailAsync(OrderDetail);
            return RedirectToPage("./Index");
        }
    }
}
