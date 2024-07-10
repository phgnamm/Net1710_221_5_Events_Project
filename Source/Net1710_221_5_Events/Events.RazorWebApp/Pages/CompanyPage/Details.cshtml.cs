using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Events.Data.Models;
using Events.Business.Business;

namespace Events.RazorWebApp.Pages.CompanyPage
{
    public class DetailsModel : PageModel
    {
        private readonly ICompanyBusiness business;

        public DetailsModel()
        {
            business ??= new CompanyBusiness();
        }

        public Company Company { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await business.GetById(id.Value);
            if (result != null && result.Status > 0 && result.Data != null)
            {
                Company = result.Data as Company;
                return Page();
            }
            return NotFound();
        }
    }
}
