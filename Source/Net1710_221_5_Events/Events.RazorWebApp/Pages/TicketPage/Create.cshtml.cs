using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Events.Data.Models;
using Events.Business.Business;
using Events.Common;

namespace Events.RazorWebApp.Pages.TicketPage
{
    public class CreateModel : PageModel
    {
        private readonly ITicketlBusiness _ticketlBusiness = new TicketBusiness();
        private readonly IOrderDetailBusiness _orderDetailBusiness = new OrderDetailBusiness();

        public List<SelectListItem> TicketTypes { get; set; }
        public string Mess {  get; set; }
        public CreateModel()
        {
        }

        public IActionResult OnGet()
        {
            TicketTypes = new List<SelectListItem>
            {
                new SelectListItem { Value = "VIP", Text = "VIP" },
                new SelectListItem { Value = "Regular", Text = "Regular" }
            };
            return Page();
        }

        [BindProperty]
        public Ticket Ticket { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Ticket.CreatedDate = DateTime.Now;
            Ticket.Status = "Active";
            /*var result = await _orderDetailBusiness.GetOrderDetailByIdAsync(Ticket.OrderDetailId);
            Ticket.OrderDetail = (OrderDetail) result.Data;*/
            var result = _ticketlBusiness.CreateTicketAsync(Ticket);
            if ((int)result.Result.Status != Const.SUCCESS_CREATE_CODE)
            {
                Mess = "Some Error had occured!";
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
