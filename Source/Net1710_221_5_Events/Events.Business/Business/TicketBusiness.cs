using Events.Business.Base;
using Events.Common;
using Events.Data.Models;
using Events.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

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
                return new EventsAppResult(Const.SUCCESS_CREATE_CODE, "Ticket created successfully", ticket);
            }
            catch (Exception ex)
            {
                return new EventsAppResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IEventsAppResult> DeleteTicketAsync(int ticketId)
        {
            try
            {
                var ticket = await _unitOfWork.TicketRepository.GetByIdAsync(ticketId);
                if (ticket != null)
                {
                    var result = await _unitOfWork.TicketRepository.RemoveAsync(ticket);
                    if (result)
                    {
                        return new EventsAppResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
                    }
                    else
                    {
                        return new EventsAppResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                    }
                }
                else
                {
                    return new EventsAppResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
            }
            catch (Exception ex)
            {
                return new EventsAppResult(-4, ex.ToString());
            }
            /*try
            {
                var existTicket = await _unitOfWork.TicketRepository.GetByIdAsync(ticketId);
                if (existTicket == null)
                {
                    return new EventsAppResult
                    {
                        Status = Const.FAIL_DELETE_CODE,
                        Message = Const.FAIL_DELETE_MSG
                    };
                }

                existTicket.IsDelete = true;

                await _unitOfWork.TicketRepository.UpdateAsync(existTicket);

                return new EventsAppResult
                {
                    Status = Const.SUCCESS_DELETE_CODE,
                    Message = Const.SUCCESS_DELETE_MSG,
                };
            }
            catch (Exception ex)
            {
                return new EventsAppResult
                {
                    Status = Const.ERROR_EXCEPTION,
                    Message = ex.ToString(),
                };
            }*/
        }

        public async Task<IEventsAppResult> GetAllTicketsAsync()
        {
            try
            {
                var tickets = await _unitOfWork.TicketRepository.GetAllAsync();
                tickets = tickets.Where(t => t.IsDelete == false).ToList<Ticket>();
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

        public async Task<IEventsAppResult> GetTicketByIdAsync(int ticketId)
        {
            try
            {
                var ticket = await _unitOfWork.TicketRepository.GetByIdAsync(ticketId);
                if (ticket == null)
                {
                    return new EventsAppResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                return new EventsAppResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, ticket);
            }
            catch (Exception ex)
            {
                return new EventsAppResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IEventsAppResult> UpdateTicketAsync(Ticket ticket)
        {
            try
            {
                var existTicket = await _unitOfWork.TicketRepository.GetByIdAsync(ticket.TicketId);
                if (existTicket == null)
                {
                    return new EventsAppResult
                    {
                        Status = Const.FAIL_UPDATE_CODE,
                        Message = Const.FAIL_UPDATE_MSG
                    };
                }
              
                existTicket.Code = ticket.Code;
                existTicket.Qrcode = ticket.Qrcode;
                existTicket.ParticipantName = ticket.ParticipantName;
                existTicket.ParticipantMail = ticket.ParticipantMail;
                existTicket.ParticipantPhone = ticket.ParticipantPhone;
                existTicket.SpecialNote = ticket.SpecialNote;
                existTicket.TicketType = ticket.TicketType;
                existTicket.Status = ticket.Status;

                await _unitOfWork.TicketRepository.UpdateAsync(existTicket);

                return new EventsAppResult
                {
                    Status = Const.SUCCESS_UPDATE_CODE,
                    Message = Const.SUCCESS_UPDATE_MSG,
                };
            }
            catch (Exception ex)
            {
                return new EventsAppResult
                {
                    Status = Const.ERROR_EXCEPTION,
                    Message = ex.ToString(),
                };
            }

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
