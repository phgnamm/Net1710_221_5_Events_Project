using Events.Business.Base;
using Events.Common;
using Events.Data.Models;
using Events.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Business.Business
{
    public interface ITicketlBusiness
    {
        Task<IEventsAppResult> CreateTicketAsync(Ticket ticket);
        Task<IEventsAppResult> GetTicketByIdAsync(int ticketId);
        Task<IEventsAppResult> GetAllTicketsAsync();
        Task<IEventsAppResult> UpdateTicketAsync(Ticket ticket);
        Task<IEventsAppResult> DeleteTicketAsync(int ticketId);
    }

    public class TicketBusiness : ITicketlBusiness
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly OrderDetailBusiness _orderDetailBusiness;
        private readonly EventBusiness _eventBusiness;
        public TicketBusiness()
        {
            _unitOfWork = new UnitOfWork();
            _orderDetailBusiness = new OrderDetailBusiness();
            _eventBusiness = new EventBusiness();
        }
        public TicketBusiness(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _orderDetailBusiness = new OrderDetailBusiness();
            _eventBusiness = new EventBusiness();
        }

        public async Task<IEventsAppResult> CreateTicketAsync(Ticket ticket)
        {
            try
            {
                await _unitOfWork.TicketRepository.CreateAsync(ticket);
                return new EventsAppResult(0, "Ticket created successfully", ticket);
            }
            catch (Exception ex)
            {
                return new EventsAppResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public Task<IEventsAppResult> DeleteTicketAsync(int ticketId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEventsAppResult> GetAllTicketsAsync()
        {
            try
            {
                var tickets = await _unitOfWork.TicketRepository.GetAllAsync();
                foreach (var ticket in tickets)
                {
                    ticket.OrderDetail = await AssignOrderDetailToTicket(ticket.OrderDetailId);
                }
                if (!tickets.Any())
                {
                    return new EventsAppResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                return new EventsAppResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, tickets);
            }
            catch (Exception ex)
            {
                return new EventsAppResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public Task<IEventsAppResult> GetTicketByIdAsync(int ticketId)
        {
            throw new NotImplementedException();
        }

        public Task<IEventsAppResult> UpdateTicketAsync(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        private async Task<OrderDetail> AssignOrderDetailToTicket(int orderdetailid)
        {
            var result = await _orderDetailBusiness.GetOrderDetailByIdAsync(orderdetailid);
            OrderDetail orderDetail = (OrderDetail) result.Data;
            result = await _eventBusiness.GetEventById(orderDetail.EventId);
            orderDetail.Event = (Event)result.Data;
            return orderDetail;
        }
    }
}
