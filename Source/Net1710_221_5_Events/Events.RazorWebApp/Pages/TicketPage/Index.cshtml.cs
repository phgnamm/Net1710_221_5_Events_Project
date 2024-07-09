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
using static NuGet.Packaging.PackagingConstants;

namespace Events.RazorWebApp.Pages.TicketPage
{
    public class IndexModel : PageModel
    {
        private readonly ITicketlBusiness _ticketbusiness = new TicketBusiness();

        public IndexModel()
        {
        }

        public IList<Ticket> Ticket { get;set; } = default!;

        // Paging component
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        // Take 5 records per page
        public const int PageSize = 5;

        // Searching component
        [BindProperty(SupportsGet = true)]
        public string SearchField { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? StartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? EndDate { get; set; }

        public async Task OnGetAsync()
        {
            var data = await _ticketbusiness.GetAllTicketsAsync();
            if (data.Status == Const.SUCCESS_READ_CODE && data.Data != null)
            {
                var tickets = (List<Ticket>)data.Data;
                if (!string.IsNullOrEmpty(SearchTerm))
                {
                    tickets = SearchField switch
                    {
                        "Code" => tickets.Where(o => o.Code.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)).ToList(),
                        "PartName" => tickets.Where(o => o.ParticipantName.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)).ToList(),
                        "PartMail" => tickets.Where(o => o.ParticipantMail.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)).ToList(),
                        "PartPhone" => tickets.Where(o => o.ParticipantPhone.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)).ToList(),
                        _ => tickets
                    };
                }
                // Apply search from date to date
                if (StartDate.HasValue && EndDate.HasValue)
                {
                    tickets = tickets.Where(o => o.CreatedDate >= StartDate && o.CreatedDate <= EndDate).ToList();
                }
                // Chia tổng record / page size => làm tròn lên
                TotalPages = (int)Math.Ceiling(tickets.Count / (double)PageSize);
                // Lấy các record dựa theo theo page index
                Ticket = tickets.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            }
            else
            {
                Ticket = new List<Ticket>();
                TotalPages = 0;
            }
        }

        // Check next - pre page
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
    }
}
