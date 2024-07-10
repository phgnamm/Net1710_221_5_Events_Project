using Events.Data.Base;
using Events.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Data.Repository
{
    public class CompanyRepository : GenericRepository<Company>
    {
        public CompanyRepository(Net17102215EventsContext context) : base(context)
        {

        }
    }
}
