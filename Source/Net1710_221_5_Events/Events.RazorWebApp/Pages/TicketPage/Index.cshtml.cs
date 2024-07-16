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
using Microsoft.CodeAnalysis.Options;

namespace Events.RazorWebApp.Pages.TicketPage
{
    public class IndexModel : PageModel
    {
        private readonly ITicketBusiness _ticketbusiness = new TicketBusiness();

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
        public string SearchCode { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchPartName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchPartMail { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchPartPhone { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchEventName { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? StartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? EndDate { get; set; }

        public async Task OnGetAsync(int pageIndex = 1)
        {
            PageIndex = pageIndex;
            var data = await _ticketbusiness.GetAllTicketsAsync();
            if (data.Status == Const.SUCCESS_READ_CODE && data.Data != null)
            {
                var tickets = (List<Ticket>)data.Data;
                if (!string.IsNullOrEmpty(SearchCode))
                {
                    tickets = tickets.Where(o => o.Code.Contains(SearchCode, StringComparison.OrdinalIgnoreCase)).ToList();
                }
                if (!string.IsNullOrEmpty(SearchPartName))
                {
                    tickets = tickets.Where(o => o.ParticipantName.Contains(SearchPartName, StringComparison.OrdinalIgnoreCase)).ToList();
                }
                if (!string.IsNullOrEmpty(SearchPartMail))
                {
                    tickets = tickets.Where(o => o.ParticipantMail.Contains(SearchPartMail, StringComparison.OrdinalIgnoreCase)).ToList();
                }
                if (!string.IsNullOrEmpty(SearchPartPhone))
                {
                    tickets = tickets.Where(o => o.ParticipantPhone.Contains(SearchPartPhone, StringComparison.OrdinalIgnoreCase)).ToList();
                }
                if (!string.IsNullOrEmpty(SearchEventName))
                {
                    tickets = tickets.Where(o => o.OrderDetail.Event.Name.Contains(SearchEventName, StringComparison.OrdinalIgnoreCase)).ToList();
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
