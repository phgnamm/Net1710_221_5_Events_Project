using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Events.Data.Models;
using Events.Business.Business;

namespace Events.RazorWebApp.Pages.EventPage
{
    public class DetailsModel : PageModel
    {
        private readonly IEventBusiness business;

        public DetailsModel()
        {
            business ??= new EventBusiness();
        }

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
    }
}
