using Events.Business.Base;
using Events.Common;
using Events.Data.DAO;
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
        private readonly OrderDetailDAO _DAO;

        public OrderDetailBusiness()
        {
            _DAO = new OrderDetailDAO();
        }

        public async Task<IEventsAppResult> CreateOrderDetailAsync(OrderDetail orderDetail)
        {
            try
            {
                await _DAO.CreateAsync(orderDetail);
                return new EventsAppResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, orderDetail);
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
                var orderDetail = await _DAO.GetByIdAsync(orderDetailId);
                if (orderDetail != null)
                {
                    var result = await _DAO.RemoveAsync(orderDetail);
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
                var orderDetails = await _DAO.GetAllAsync();
                if (!orderDetails.Any())
                {
                    return new EventsAppResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }

                var result = orderDetails.Select(od => new
                {
                    od.OrderDetailId,
                    od.Quantity,
                    od.Price,
                    Event = new
                    {
                        od.Event.Name,
                        od.Event.Location,
                        od.Event.StartDate,
                        od.Event.EndDate
                    },
                    Order = new
                    {
                        od.Order.Code,
                        od.Order.PaymentStatus
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
                var orderDetail = await _DAO.GetByIdAsync(orderDetailId);
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
                var existingOrderDetail = await _DAO.GetByIdAsync(orderDetail.OrderDetailId);
                if (existingOrderDetail == null)
                {
                    return new EventsAppResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                }

                existingOrderDetail.Quantity = orderDetail.Quantity;
                existingOrderDetail.Price = orderDetail.Price;
                existingOrderDetail.EventId = orderDetail.EventId;
                existingOrderDetail.OrderId = orderDetail.OrderId;

                await _DAO.UpdateAsync(existingOrderDetail);

                return new EventsAppResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, existingOrderDetail);
            }
            catch (Exception ex)
            {
                return new EventsAppResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }
    }
}
