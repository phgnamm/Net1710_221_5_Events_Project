using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Events.Data.Models;

namespace Events.RazorWebApp.Pages.OrderDetailPage
{
    public class DeleteModel : PageModel
    {
        private readonly Events.Data.Models.Net17102215EventsContext _context;

        public DeleteModel(Events.Data.Models.Net17102215EventsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OrderDetail OrderDetail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderdetail = await _context.OrderDetails.FirstOrDefaultAsync(m => m.OrderDetailId == id);

            if (orderdetail == null)
            {
                return NotFound();
            }
            else
            {
                OrderDetail = orderdetail;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderdetail = await _context.OrderDetails.FindAsync(id);
            if (orderdetail != null)
            {
                OrderDetail = orderdetail;
                _context.OrderDetails.Remove(OrderDetail);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
