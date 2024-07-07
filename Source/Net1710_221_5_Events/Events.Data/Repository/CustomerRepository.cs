using Events.Data.Base;
using Events.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Data.Repository
{
    public class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository(Net17102215EventsContext context) : base(context)
        {

        }

        public async Task<Customer> GetCustomerByEmail(string email)
        {
            return await _context.Customers.FirstOrDefaultAsync(e => e.Email == email);
        }
        public async Task<Customer> GetCustomerByPhoneNumber(string phoneNumber)
        {
            return await _context.Customers.FirstOrDefaultAsync(e => e.PhoneNumber == phoneNumber);
        }
    }
}
