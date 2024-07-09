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
    public class DeleteModel : PageModel
    {
        private readonly ITicketlBusiness _ticketlBusiness = new TicketBusiness();

        public DeleteModel()
        {
        }

        [BindProperty]
        public Ticket Ticket { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _ticketlBusiness.GetTicketByIdAsync((int) id);

            if (ticket == null)
            {
                return NotFound();
            }
            else
            {
                Ticket = (Ticket) ticket.Data;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _ticketlBusiness.DeleteTicketAsync((int) id);
            if (result.Status == Const.SUCCESS_DELETE_CODE)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the ticket.");
                return Page();
            }
        }
    }
}
