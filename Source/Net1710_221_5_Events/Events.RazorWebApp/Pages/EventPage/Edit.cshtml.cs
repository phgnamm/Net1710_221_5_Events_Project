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

namespace Events.RazorWebApp.Pages.EventPage
{
    public class EditModel : PageModel
    {
        private readonly IEventBusiness business;

        public EditModel()
        {
            business ??= new EventBusiness();
        }

        [BindProperty]
        public Event Event { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await business.GetEventById(id.Value);
            if (result != null && result.Status > 0 && result.Data != null)
            {
                Event = result.Data as Event;
                return Page();
            }
            return NotFound();

        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Event.StartDate.Date >= Event.EndDate.Date)
            {
                ModelState.AddModelError(string.Empty, "Start date must be before End date.");
                return Page();
            }

            if (Event.OpenTicket.Date >= Event.CloseTicket.Date)
            {
                ModelState.AddModelError(string.Empty, "Open ticket date must be before Close ticket date.");
                return Page();
            }

            if (Event.OpenTicket.Date >= Event.StartDate.Date)
            {
                ModelState.AddModelError(string.Empty, "Open ticket date must be before Start date.");
                return Page();
            }

            //_context.Attach(Event).State = EntityState.Modified;

            try
            {
                await business.UpdateEventById(Event);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(Event.EventId).Result)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> EventExists(int id)
        {
            var result = await business.GetEventById(id);
            if (result != null && result.Status > 0 && result.Data != null)
            {
                return true;
            }

            return false;
        }
    }
}
