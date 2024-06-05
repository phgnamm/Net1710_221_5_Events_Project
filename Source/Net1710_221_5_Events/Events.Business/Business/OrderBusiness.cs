using Events.Business.Base;
using Events.Common;
using Events.Data;
using Events.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Business
{
    public interface IOrderBusiness
    {
        public Task<IEventsAppResult> GetAllOrders();
        public Task<IEventsAppResult> GetOrderById(int id);
        public Task<IEventsAppResult> CreateNewOrder(Order newOrder);
        public Task<IEventsAppResult> UpdateOrderById(Order updateOrder);
        public Task<IEventsAppResult> DeleteOrderById(int id);
    }

    public class OrderBusiness : IOrderBusiness
    {
        private readonly UnitOfWork _unitOfWork;

        public OrderBusiness()
        {
            _unitOfWork =  new UnitOfWork();
        }

        public async Task<IEventsAppResult> CreateNewOrder(Order newOrder)
        {
            try
            {
                await _unitOfWork.OrderRepository.CreateAsync(newOrder);
                return new EventsAppResult
                {
                    Status = Const.SUCCESS_CREATE_CODE,
                    Message = Const.SUCCESS_CREATE_MSG,
                    Data = newOrder
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

        public async Task<IEventsAppResult> DeleteOrderById(int id)
        {
            try
            {
                var currency = await _unitOfWork.OrderRepository.GetByIdAsync(id);
                if (currency != null)
                {
                    var result = await _unitOfWork.OrderRepository.RemoveAsync(currency);
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
        }

        public async Task<IEventsAppResult> GetAllOrders()
        {
            try
            {
                var orders = await _unitOfWork.OrderRepository.GetAllAsync();
                if (!orders.Any())
                {
                    return new EventsAppResult
                    {
                        Status = Const.WARNING_NO_DATA_CODE,
                        Message = Const.WARNING_NO_DATA__MSG,
                    };
                }
                return new EventsAppResult
                {
                    Status = Const.SUCCESS_READ_CODE,
                    Message = Const.SUCCESS_READ_MSG,
                    Data = orders.ToList()
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

        public async Task<IEventsAppResult> GetOrderById(int id)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
                if (order == null)
                {
                    return new EventsAppResult
                    {
                        Status = Const.WARNING_NO_DATA_CODE,
                        Message = Const.WARNING_NO_DATA__MSG
                    };
                }
                return new EventsAppResult
                {
                    Status = Const.SUCCESS_READ_CODE,
                    Message = Const.SUCCESS_READ_MSG,
                    Data = order
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

        public async Task<IEventsAppResult> UpdateOrderById(Order updateOrder)
        {
            try
            {
                var existOrder = await _unitOfWork.OrderRepository.GetByIdAsync(updateOrder.OrderId);
                if (existOrder == null)
                {
                    return new EventsAppResult
                    {
                        Status = Const.FAIL_UPDATE_CODE,
                        Message = Const.FAIL_UPDATE_MSG
                    };
                }
                // edit info order
                existOrder.Code = updateOrder.Code;
                existOrder.Description = updateOrder.Description;
                existOrder.TotalAmount = updateOrder.TotalAmount;
                existOrder.TicketQuantity = updateOrder.TicketQuantity;
                existOrder.PaymentStatus = updateOrder.PaymentStatus;
                existOrder.PaymentMethod = updateOrder.PaymentMethod;
                existOrder.PaymentDate = updateOrder.PaymentDate;
                existOrder.Status = updateOrder.Status;

                // save order
                await _unitOfWork.OrderRepository.UpdateAsync(existOrder);

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
    }
}
