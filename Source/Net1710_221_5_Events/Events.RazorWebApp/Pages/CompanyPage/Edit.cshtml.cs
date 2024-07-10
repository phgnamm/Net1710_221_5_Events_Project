using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Events.Data.Models;
using Events.Business.Business;

namespace Events.RazorWebApp.Pages.CompanyPage
{
    public class EditModel : PageModel
    {
        private readonly ICompanyBusiness business;

        public EditModel()
        {
            business ??= new CompanyBusiness();
        }

        [BindProperty]
        public Company Company { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company =  await business.GetById(id.Value);
            if (company == null)
            {
                return NotFound();
            }
            Company = company.Data as Company;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await business.Update(Company);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(Company.CompanyId).Result)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
           
        }

        private async Task<bool> CompanyExists(int id)
        {
            var result = await business.GetById(id);
            if (result != null && result.Status > 0 && result.Data != null)
            {
                return true;
            }

            return false;
        }
    }
}
