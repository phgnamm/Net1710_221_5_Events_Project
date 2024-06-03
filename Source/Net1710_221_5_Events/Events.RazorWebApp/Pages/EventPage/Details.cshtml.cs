using Events.Business;
using Events.Business.Business;
using Events.Common;
using Events.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Events.RazorWebApp.Pages.EventPage
{
    public class DetailsModel : PageModel
    {
        //private readonly IEventBusiness _eventBusiness;

        private readonly IEventBusiness _eventBusiness = new EventBusiness();

        public Event Event { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _eventBusiness.GetEventById(id);
            if (result.Status == Const.SUCCESS_READ_CODE)
            {
                Event = (Event)result.Data;
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
    }
}
