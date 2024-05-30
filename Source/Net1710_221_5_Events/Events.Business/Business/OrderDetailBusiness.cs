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
        public OrderDetailBusiness()
        {
            _unitOfWork = new UnitOfWork();
        }
        public OrderDetailBusiness(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEventsAppResult> CreateOrderDetailAsync(OrderDetail orderDetail)
        {
            try
            {
                await _unitOfWork.OrderDetailRepository.CreateAsync(orderDetail);
                return new EventsAppResult(0, "OrderDetail created successfully", orderDetail);
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
                if (!orderDetails.Any())
                {
                    return new EventsAppResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }

                var result = orderDetails.Select(od => new OrderDetailDto
                {
                    OrderDetailId = od.OrderDetailId,
                    Quantity = od.Quantity,
                    Price = od.Price,
                    Event = new EventDto
                    {
                        Name = od.Event.Name,
                        Location = od.Event.Location,
                        StartDate = od.Event.StartDate,
                        EndDate = od.Event.EndDate
                    },
                    Order = new OrderDto
                    {
                        Code = od.Order.Code,
                        PaymentStatus = od.Order.PaymentStatus
                    }
                }).ToList();

                return new EventsAppResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result);
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

                var result = new
                {
                    orderDetail.OrderDetailId,
                    orderDetail.Quantity,
                    orderDetail.Price,
                    Event = new
                    {
                        orderDetail.Event.Name,
                        orderDetail.Event.Location,
                        orderDetail.Event.StartDate,
                        orderDetail.Event.EndDate
                    },
                    Order = new
                    {
                        orderDetail.Order.Code,
                        orderDetail.Order.PaymentStatus
                    }
                };

                return new EventsAppResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result);
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
    }
}
