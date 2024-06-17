using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Events.Data.Models;
using Events.Business.Business;
using Events.Business.Base;

namespace Events.RazorWebApp.Pages.EventPage
{
    public class IndexModel : PageModel
    {
        private readonly IEventBusiness business;

        public IndexModel()
        {
            business ??= new EventBusiness();
        }

        public IList<Event> Event { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchType { get; set; }

        public Pagination Pagination { get; set; } = new Pagination();

        public async Task OnGetAsync(int pageIndex = 1)
        {

            Pagination.PageIndex = pageIndex;

            var result = await business.GetAllEvents();
            if (result != null && result.Status > 0 && result.Data != null)
            {
                var events = result.Data as List<Event>;

                if (!string.IsNullOrEmpty(SearchType) && !string.IsNullOrEmpty(Search))
                {
                    if (SearchType == "name")
                    {
                        events = events.Where(e => e.Name.Contains(Search, StringComparison.OrdinalIgnoreCase)).ToList();
                    }
                    else if (SearchType == "location")
                    {
                        events = events.Where(e => e.Location.Contains(Search, StringComparison.OrdinalIgnoreCase)).ToList();
                    }
                    else
                    {
                        events = events.Where(e => e.OperatorName.Contains(Search, StringComparison.OrdinalIgnoreCase)).ToList();
                    }
                }

                Pagination.TotalPages = (int)Math.Ceiling(events.Count / (double)Pagination.PageSize);

                Event = events
                    .Skip((Pagination.PageIndex - 1) * Pagination.PageSize)
                    .Take(Pagination.PageSize)
                    .ToList();
            }
        }
    }
}
