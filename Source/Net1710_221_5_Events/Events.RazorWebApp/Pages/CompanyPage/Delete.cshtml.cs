﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Events.Data.Models;
using Events.Business.Business;
using Events.Common;

namespace Events.RazorWebApp.Pages.CompanyPage
{
    public class DeleteModel : PageModel
    {
        private readonly ICompanyBusiness business;
        public DeleteModel()
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

            var result = await business.GetById(id.Value);
            if (result != null && result.Status > 0 && result.Data != null)
            {
                Company = result.Data as Company;
                return Page();
            }
            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            var result = await business.Delete(id.Value);
            if (result.Status == Const.SUCCESS_DELETE_CODE)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the customer.");
                return Page();
            }
        }
    }
}
