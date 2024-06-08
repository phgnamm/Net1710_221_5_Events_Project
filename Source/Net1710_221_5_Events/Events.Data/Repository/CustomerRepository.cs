using Events.Data.Base;
using Events.Data.Models;
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
    }
}
