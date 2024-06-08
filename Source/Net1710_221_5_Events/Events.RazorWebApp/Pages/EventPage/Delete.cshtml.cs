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

namespace Events.RazorWebApp.Pages.EventPage
{
    public class DeleteModel : PageModel
    {
        private readonly IEventBusiness business;

        public DeleteModel()
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await business.DeleteEventById(id.Value);
            if (result.Status == Const.SUCCESS_DELETE_CODE)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the Event.");
                return Page();
            }
        }
    }
}
