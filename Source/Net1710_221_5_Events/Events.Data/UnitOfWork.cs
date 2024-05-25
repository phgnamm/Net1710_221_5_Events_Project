using Events.Data.Models;
using Events.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Data
{
    public class UnitOfWork
    {
        private Net17102215EventsContext _unitOfWorkContext;
        private OrderDetailRepository _orderDetail;
        public UnitOfWork() 
        { 
        }
        public OrderDetailRepository OrderDetailRepository
        {
            get
            {
                return _orderDetail ??= new Repository.OrderDetailRepository();
            }
        }
    }
}
