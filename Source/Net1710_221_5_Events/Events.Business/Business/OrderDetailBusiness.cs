using Events.Business.Base;
using Events.Common;
using Events.Data;
using Events.Data.DTOs;
using Events.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Business.Business
{
    public interface IOrderDetailBusiness
    {
        Task<IEventsAppResult> CreateOrderDetailAsync(OrderDetail orderDetail);
        Task<IEventsAppResult> GetOrderDetailByIdAsync(int orderDetailId);
        Task<IEventsAppResult> GetAllOrderDetailsAsync();
        Task<IEventsAppResult> UpdateOrderDetailAsync(OrderDetail orderDetail);
        Task<IEventsAppResult> DeleteOrderDetailAsync(int orderDetailId);
    }

    public class OrderDetailBusiness : IOrderDetailBusiness
    {

        private readonly UnitOfWork _unitOfWork;
        private IOrderBusiness _orderBusiness;
        private IEventBusiness _eventBusiness;
        public OrderDetailBusiness()
        {
            _unitOfWork = new UnitOfWork();
            _orderBusiness = new OrderBusiness();
            _eventBusiness = new EventBusiness();
        }
        public async Task<IEventsAppResult> CreateOrderDetailAsync(OrderDetail orderDetail)
        {
            try
            {
                await _unitOfWork.OrderDetailRepository.CreateAsync(orderDetail);
                return new EventsAppResult(Const.SUCCESS_CREATE_CODE, "OrderDetail created successfully", orderDetail);
            }
            catch (Exception ex)
            {
                return new EventsAppResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IEventsAppResult> DeleteOrderDetailAsync(int orderDetailId)
        {
            try
            {
                var orderDetail = await _unitOfWork.OrderDetailRepository.GetByIdAsync(orderDetailId);
                if (orderDetail != null)
                {
                    var result = await _unitOfWork.OrderDetailRepository.RemoveAsync(orderDetail);
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
                return new EventsAppResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IEventsAppResult> GetAllOrderDetailsAsync()
        {
            try
            {
                var orderDetails = await _unitOfWork.OrderDetailRepository.GetAllAsync();
                foreach (var orderdetail in orderDetails)
                {
                    orderdetail.Event = await AssignEventtoOrderDetail(orderdetail);
                    orderdetail.Order = await AssignOrdertoOrderDetail(orderdetail);

                }
                if (orderDetails == null)
                {
                    return new EventsAppResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                return new EventsAppResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, orderDetails);
            }
            catch (Exception ex)
            {
                return new EventsAppResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }


        public async Task<IEventsAppResult> GetOrderDetailByIdAsync(int orderDetailId)
        {
            try
            {
                var orderDetail = await _unitOfWork.OrderDetailRepository.GetByIdAsync(orderDetailId);
                if (orderDetail == null)
                {
                    return new EventsAppResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                return new EventsAppResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, orderDetail);
            }
            catch (Exception ex)
            {
                return new EventsAppResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IEventsAppResult> UpdateOrderDetailAsync(OrderDetail orderDetail)
        {
            try
            {
                var existingOrderDetail = await _unitOfWork.OrderDetailRepository.GetByIdAsync(orderDetail.OrderDetailId);
                if (existingOrderDetail == null)
                {
                    return new EventsAppResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                }

                existingOrderDetail.Quantity = orderDetail.Quantity;
                existingOrderDetail.Price = orderDetail.Price;
                existingOrderDetail.EventId = orderDetail.EventId;
                existingOrderDetail.OrderId = orderDetail.OrderId;

                await _unitOfWork.OrderDetailRepository.UpdateAsync(existingOrderDetail);

                return new EventsAppResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, existingOrderDetail);
            }
            catch (Exception ex)
            {
                return new EventsAppResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        private async Task<Event> AssignEventtoOrderDetail(OrderDetail orderdetail)
        {
            var result = await _eventBusiness.GetEventById(orderdetail.EventId);
            return (Event)result.Data;
        }
        private async Task<Order> AssignOrdertoOrderDetail(OrderDetail orderdetail)
        {
            var result = await _orderBusiness.GetOrderById(orderdetail.OrderId);
            return (Order)result.Data;
        }
    }
}
