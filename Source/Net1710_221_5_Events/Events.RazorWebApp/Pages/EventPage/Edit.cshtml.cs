using Events.Business.Business;
using Events.Common;
using Events.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Events.RazorWebApp.Pages.EventPage
{
    public class EditModel : PageModel
    {
        private readonly IEventBusiness _eventBusiness = new EventBusiness();

        [BindProperty]
        public Event Event { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var result = await _eventBusiness.GetEventById(id);
            if (result.Status == Const.SUCCESS_READ_CODE)
            {
                Event = (Event) result.Data;
                if (Event == null)
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await _eventBusiness.UpdateEventById(Event);
            if (result.Status == Const.SUCCESS_UPDATE_CODE)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while updating the event.");
                return Page();
            }
        }
    }
}
