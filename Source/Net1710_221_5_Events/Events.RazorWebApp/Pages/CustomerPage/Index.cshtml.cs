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
using Events.Business.Base;

namespace Events.RazorWebApp.Pages.CustomerPage
{
    public class IndexModel : PageModel
    {
        private readonly ICustomerBusiness business;

        public IndexModel()
        {
            business ??= new CustomerBusiness();
        }

        public IList<Customer> Customer { get;set; } = new List<Customer>();
        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchType { get; set; }

        public Pagination Pagination { get; set; } = new Pagination();
        public async Task OnGetAsync(int pageIndex = 1)
        {
            Pagination.PageIndex = pageIndex;
            var result = await business.GetAllCustomers();
            if (result != null && result.Status > 0 && result.Data != null)
            {
                var customers = result.Data as List<Customer>;
                Customer = customers;

                if (!string.IsNullOrEmpty(Search))
                {
                    customers = ApplySearchCriteria(customers, Search);
                }

                Pagination.TotalPages = (int)Math.Ceiling(customers.Count / (double)Pagination.PageSize);
                Customer = customers
                            .Skip((Pagination.PageIndex - 1) * Pagination.PageSize)
                            .Take(Pagination.PageSize)
                            .ToList();
            }
            else
            {
                Customer = new List<Customer>();
            }
        }

        private List<Customer> ApplySearchCriteria(List<Customer> customers, string search)
        {
            var criteria = search.Split(',').Select(c => c.Trim()).Where(c => !string.IsNullOrEmpty(c)).ToList();

            if (criteria.Count > 0)
            {
                customers = customers.Where(e =>
                    (criteria.Count > 0 && e.FullName.Contains(criteria[0], StringComparison.OrdinalIgnoreCase)) ||
                    (criteria.Count > 1 && e.PhoneNumber.Contains(criteria[1], StringComparison.OrdinalIgnoreCase)) ||
                    (criteria.Count > 2 && e.Email.Contains(criteria[2], StringComparison.OrdinalIgnoreCase)) ||
                    (criteria.Count > 3 && e.City.Contains(criteria[3], StringComparison.OrdinalIgnoreCase)) ||
                    (criteria.Count > 4 && e.Country.Contains(criteria[4], StringComparison.OrdinalIgnoreCase))
                ).ToList();
            }

            return customers;
        }


    }
}
