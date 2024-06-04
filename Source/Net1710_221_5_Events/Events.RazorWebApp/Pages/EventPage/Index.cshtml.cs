using Events.Business;
using Events.Business.Business;
using Events.Common;
using Events.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Events.RazorWebApp.Pages.EventPage
{
    public class IndexModel : PageModel
    {

        private readonly IEventBusiness _eventBusiness = new EventBusiness();

        public IList<Event> Events { get; set; } = new List<Event>();

        public async Task OnGetAsync()
        {
            var result = await _eventBusiness.GetAllEvents();
            if (result.Status == Const.SUCCESS_READ_CODE)
            {
                Events = (IList<Event>)result.Data;
            }
            else
            {
                Events = new List<Event>();
            }
        }
    }
}
