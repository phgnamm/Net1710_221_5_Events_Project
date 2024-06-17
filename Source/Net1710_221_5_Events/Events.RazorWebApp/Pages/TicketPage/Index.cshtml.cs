using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Events.Data.Models;
using Events.Business.Business;
using Events.Common;

namespace Events.RazorWebApp.Pages.TicketPage
{
    public class IndexModel : PageModel
    {
        private readonly ITicketlBusiness _ticketbusiness = new TicketBusiness();

        public IndexModel()
        {
        }

        public IList<Ticket> Ticket { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var tickets = await _ticketbusiness.GetAllTicketsAsync();
            if (tickets.Status == Const.SUCCESS_READ_CODE && tickets.Data != null)
            {
                Ticket = (List<Ticket>)tickets.Data;
            }
        }
    }
}
