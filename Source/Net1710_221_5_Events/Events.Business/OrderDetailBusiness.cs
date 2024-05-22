using Events.Data.DAO;
using Events.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Business
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
        /*private readonly Net17102215EventsContext _context;*/
        private readonly OrderDetailDAO _DAO;

        /*   public OrderDetailBusiness(Net17102215EventsContext context)
           {
               _context = context;
           }
        */
        public OrderDetailBusiness()
        {
            _DAO = new OrderDetailDAO();
        }

        public async Task<IEventsAppResult> CreateOrderDetailAsync(OrderDetail orderDetail)
        {
            try
            {
                /*_context.OrderDetails.Add(orderDetail);
                await _context.SaveChangesAsync();*/
                await _DAO.CreateAsync(orderDetail);
                return new EventsAppResult(0, "OrderDetail created successfully", orderDetail);
            }
            catch (Exception ex)
            {
                return new EventsAppResult(-1, $"Failed to create OrderDetail: {ex.Message}");
            }
        }

        public async Task<IEventsAppResult> GetOrderDetailByIdAsync(int orderDetailId)
        {
            try
            {
                /* var orderDetail = await _context.OrderDetails
                     .Include(od => od.Event)
                     .Include(od => od.Order)
                     .FirstOrDefaultAsync(od => od.OrderDetailId == orderDetailId);*/
                var orderDetail = _DAO.GetByIdAsync(orderDetailId);

                if (orderDetail == null)
                {
                    return new EventsAppResult(-1, "OrderDetail not found");
                }

                return new EventsAppResult(0, "OrderDetail retrieved successfully", orderDetail);
            }
            catch (Exception ex)
            {
                return new EventsAppResult(-1, $"Failed to retrieve OrderDetail: {ex.Message}");
            }
        }

        public async Task<IEventsAppResult> GetAllOrderDetailsAsync()
        {
            try
            {
                var orderDetails = _DAO.GetAllAsync();
                /*var orderDetails = await _context.OrderDetails
                    .Include(od => od.Event)
                    .Include(od => od.Order)
                    .ToListAsync();*/

                return new EventsAppResult(0, "All OrderDetails retrieved successfully", orderDetails);
            }
            catch (Exception ex)
            {
                return new EventsAppResult(-1, $"Failed to retrieve OrderDetails: {ex.Message}");
            }
        }

        public async Task<IEventsAppResult> UpdateOrderDetailAsync(OrderDetail orderDetail)
        {
            try
            {
                /* _context.OrderDetails.Update(orderDetail);
                 await _context.SaveChangesAsync();*/
                await _DAO.UpdateAsync(orderDetail);
                return new EventsAppResult(0, "OrderDetail updated successfully", orderDetail);
            }
            catch (Exception ex)
            {
                return new EventsAppResult(-1, $"Failed to update OrderDetail: {ex.Message}");
            }
        }

        public async Task<IEventsAppResult> DeleteOrderDetailAsync(int orderDetailId)
        {
            try
            {
                /* var orderDetail = await _context.OrderDetails.FindAsync(orderDetailId);*/
                var orderDetail = _DAO.GetByIdAsync(orderDetailId);
                if (orderDetail == null)
                {
                    return new EventsAppResult(-1, "OrderDetail not found");
                }

                /*_context.OrderDetails.Remove(orderDetail);
                await _context.SaveChangesAsync();*/

                return new EventsAppResult(0, "OrderDetail deleted successfully", orderDetail);
            }
            catch (Exception ex)
            {
                return new EventsAppResult(-1, $"Failed to delete OrderDetail: {ex.Message}");
            }
        }
    }
}

