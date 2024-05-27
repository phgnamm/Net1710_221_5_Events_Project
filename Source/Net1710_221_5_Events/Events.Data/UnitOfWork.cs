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
        private readonly Net17102215EventsContext _unitOfWorkContext;
        private EventRepository _eventRepository;
        private OrderRepository _orderRepository;
        private OrderDetailRepository _orderDetailRepository;

        public UnitOfWork(Net17102215EventsContext unitOfWorkContext)
        {
            _unitOfWorkContext = unitOfWorkContext;
        }

        public EventRepository EventRepository => _eventRepository ??= new EventRepository(_unitOfWorkContext);
        public OrderRepository OrderRepository => _orderRepository ??= new OrderRepository(_unitOfWorkContext);
        public OrderDetailRepository OrderDetailRepository => _orderDetailRepository ??= new OrderDetailRepository(_unitOfWorkContext);

    }
}
