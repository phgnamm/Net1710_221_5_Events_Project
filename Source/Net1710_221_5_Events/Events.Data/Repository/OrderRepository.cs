using Events.Data.Base;
using Events.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Data.Repository
{
    public class OrderRepository : GenericRepository<Order>
    {
        public OrderRepository(Net17102215EventsContext context) : base(context)
        {
        }
    }

}
