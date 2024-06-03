using Events.Business.Business;
using Events.Common;
using Events.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Events.RazorWebApp.Pages.EventPage
{
    public class CreateModel : PageModel
    {
        private readonly IEventBusiness _eventBusiness = new EventBusiness();

        [BindProperty]
        public Event Event { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await _eventBusiness.CreateNewEvent(Event);
            if (result.Status == Const.SUCCESS_CREATE_CODE)
            {
                return RedirectToPage("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the Event.");
                return Page();
            }
        }
    }
}
