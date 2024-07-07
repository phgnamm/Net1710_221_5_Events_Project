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
                if(!string.IsNullOrEmpty(SearchType) && !string.IsNullOrEmpty(Search))
                {
                    if(SearchType == "name")
                    {
                        customers = customers.Where(e => e.FullName.Contains(Search, StringComparison.OrdinalIgnoreCase)).ToList();
                    } 
                    else if(SearchType == "phoneNumber")
                    {
                        customers = customers.Where(e => e.PhoneNumber.Contains(Search, StringComparison.OrdinalIgnoreCase)).ToList();
                    }
                    else if (SearchType == "email")
                    {
                        customers = customers.Where(e => e.Email.Contains(Search, StringComparison.OrdinalIgnoreCase)).ToList();
                    }
                    else if (SearchType == "city")
                    {
                        customers = customers.Where(e => e.City.Contains(Search, StringComparison.OrdinalIgnoreCase)).ToList();
                    }
                    else if (SearchType == "country")
                    {
                        customers = customers.Where(e => e.Country.Contains(Search, StringComparison.OrdinalIgnoreCase)).ToList();
                    }
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
    }
}
