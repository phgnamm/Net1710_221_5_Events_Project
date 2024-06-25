using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Events.Data.Models;
using Events.Business;
using Events.Common;
using Events.RazorWebApp.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Events.RazorWebApp.Pages.OrderPage
{
    public class IndexModel : PageModel
    {
        private readonly IOrderBusiness _orderBusiness;

        public IndexModel(IOrderBusiness orderBusiness)
        {
            _orderBusiness = orderBusiness;
        }

        public IList<Order> Order { get; set; } = new List<Order>();

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


        // Render page
        public async Task OnGetAsync(int pageIndex = 1)
        {
            PageIndex = pageIndex;

            var result = await _orderBusiness.GetAllOrders();
            if (result.Status == Const.SUCCESS_READ_CODE)
            {
                var orders = (IList<Order>)result.Data;

                // Apply search by
                if (!string.IsNullOrEmpty(SearchTerm))
                {
                    orders = SearchField switch
                    {
                        "Code" => orders.Where(o => o.Code.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)).ToList(),
                        "Description" => orders.Where(o => o.Description.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)).ToList(),
                        "PaymentStatus" => orders.Where(o => o.PaymentStatus.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)).ToList(),
                        "PaymentMethod" => orders.Where(o => o.PaymentMethod.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)).ToList(),
                        _ => orders
                    };
                }
                // Apply search from date to date
                if (StartDate.HasValue && EndDate.HasValue)
                {
                    orders = orders.Where(o => o.PaymentDate >= StartDate && o.PaymentDate <= EndDate).ToList();
                }
                // Chia tổng record / page size => làm tròn lên
                TotalPages = (int)Math.Ceiling(orders.Count / (double)PageSize);
                // Lấy các record dựa theo theo page index
                Order = orders.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            }
            else
            {
                Order = new List<Order>();
                TotalPages = 0;
            }
        }

        // Check next - pre page
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
    }
}
