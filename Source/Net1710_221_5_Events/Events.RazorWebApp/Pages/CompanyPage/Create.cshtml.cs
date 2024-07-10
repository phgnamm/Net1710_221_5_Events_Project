using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Events.Data.Models;
using Events.Business.Business;

namespace Events.RazorWebApp.Pages.CompanyPage
{
    public class CreateModel : PageModel
    {
        private readonly ICompanyBusiness business;

        public CreateModel()
        {
            business ??= new CompanyBusiness();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Company Company { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await business.Create(Company);

            return RedirectToPage("./Index");
        }
    }
}
