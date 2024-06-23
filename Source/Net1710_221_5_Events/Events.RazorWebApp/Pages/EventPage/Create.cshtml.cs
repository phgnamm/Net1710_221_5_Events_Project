using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Events.Data.Models;
using Events.Business.Business;

namespace Events.RazorWebApp.Pages.EventPage
{
    public class CreateModel : PageModel
    {
        private readonly IEventBusiness business;

        public CreateModel()
        {
            business ??= new EventBusiness();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Event Event { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
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

            await business.CreateNewEvent(Event);

            return RedirectToPage("./Index");
        }
    }
}
