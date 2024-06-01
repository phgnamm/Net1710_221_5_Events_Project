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
    public class OrderDetailRepository : GenericRepository<OrderDetail>
    {
        public OrderDetailRepository(Net17102215EventsContext context) : base(context)
        {
        }

        public async Task<List<OrderDetail>> GetAllAsync()
        {
            return await _dbSet.Include(od => od.Event)
                               .Include(od => od.Order)
                               .ToListAsync();
        }
    }
}
