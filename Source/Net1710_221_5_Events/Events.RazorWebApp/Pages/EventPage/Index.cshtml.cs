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
    public class IndexModel : PageModel
    {
        private readonly IEventBusiness business;

        public IndexModel()
        {
            business ??= new EventBusiness();
        }

        public IList<Event> Event { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var result = await business.GetAllEvents();
            if (result != null && result.Status > 0 && result.Data != null) 
            {
                Event = result.Data as List<Event>;
            }
        }
    }
}
