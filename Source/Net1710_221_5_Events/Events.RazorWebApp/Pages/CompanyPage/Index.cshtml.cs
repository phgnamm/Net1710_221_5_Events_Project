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
    public class IndexModel : PageModel
    {
        private readonly ICompanyBusiness business;

        public IndexModel()
        {
            business ??= new CompanyBusiness();
        }

        public IList<Company> Company { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var result = await business.GetAll();
            if (result != null && result.Status > 0 && result.Data != null)
            {
                Company = result.Data as List<Company>;
            }
            else
            {
                Company = new List<Company>();
            }

        }
    }
}
