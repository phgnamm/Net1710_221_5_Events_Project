using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Events.Data.Models;
using Events.Business.Business;
using Events.Common;

namespace Events.RazorWebApp.Pages.TicketPage
{
    public class EditModel : PageModel
    {
        private readonly ITicketBusiness _ticketBusiness = new TicketBusiness();

        public EditModel()
        {
            TicketTypes = new List<SelectListItem>
            {
                new SelectListItem { Value = "VIP", Text = "VIP" },
                new SelectListItem { Value = "Regular", Text = "Regular" }
            };
            Statuses = new List<SelectListItem>
            {
                new SelectListItem {Value = "Active", Text = "Active"},
                new SelectListItem {Value = "Checked in", Text ="Checked in"},
                new SelectListItem {Value = "Inactive", Text = "Inactive"}
            };
        }

        [BindProperty]
        public Ticket Ticket { get; set; } = default!;

        public List<SelectListItem> TicketTypes { get; set; }

        public List<SelectListItem> Statuses { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result =  await _ticketBusiness.GetTicketByIdAsync((int)id);
            if (result == null)
            {
                return NotFound();
            }
            Ticket = (Ticket) result.Data;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var result = await _ticketBusiness.UpdateTicketAsync(Ticket);
            if (result.Status == Const.SUCCESS_UPDATE_CODE)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while updating the ticket.");
                return Page();
            }
        }

        private bool TicketExists(int id)
        {
            return _ticketBusiness.GetTicketByIdAsync(id) != null;
        }
    }
}
