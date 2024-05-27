﻿using Events.Data.Base;
using Events.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Data.Repository
{
    public class OrderDetailRepository: GenericRepository<OrderDetail>
    {
        public OrderDetailRepository(Net17102215EventsContext context) : base(context)
        { 
        }
    }
}
