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

            await business.CreateNewEvent(Event);

            return RedirectToPage("./Index");
        }
    }
}
