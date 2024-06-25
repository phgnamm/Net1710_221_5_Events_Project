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
    public class TicketRepository : GenericRepository<Ticket>
    {
        public TicketRepository(Net17102215EventsContext context) : base(context)
        { }
    }
}
