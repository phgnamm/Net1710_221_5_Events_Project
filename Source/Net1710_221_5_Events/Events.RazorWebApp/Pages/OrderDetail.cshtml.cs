using Events.Business;
using Events.Business.Business;
using Events.Common;
using Events.Data.DTOs;
using Events.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Events.RazorWebApp.Pages
{
    public class OrderDetailModel : PageModel
    {
        private readonly IOrderDetailBusiness _orderDetailBusiness = new OrderDetailBusiness();
        private readonly IEventBusiness _eventBusiness = new EventBusiness();
        private readonly IOrderBusiness _orderBusiness = new OrderBusiness();
        [TempData]
        public string Message { get; set; } = default;
        [BindProperty]
        public OrderDetail OrderDetail { get; set; } = default;
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public List<Event> Events { get; set; } = new List<Event>();
        public List<Order> Orders { get; set; } = new List<Order>();

        public int PageIndex { get; set; } = 1;
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 4;

        public void OnGet(int pageIndex = 1)
        {
            PageIndex = pageIndex;
            OrderDetails = GetPaginatedOrderDetails(PageIndex, PageSize);
            Events = GetEvents();
            Orders = GetOrders();
        }

        public IActionResult OnPost()
        {
            SaveOrderDetail();
            return RedirectToPage("/OrderDetail");
        }
        public IActionResult OnPostEdit()
        {
            UpdateOrderDetail();
            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int id)
        {
            DeleteOrderDetail(id);
            return RedirectToPage();
        }
        private List<OrderDetail> GetOrderDetails()
        {
            var orderDetailResult = _orderDetailBusiness.GetAllOrderDetailsAsync().Result;
            return (List<OrderDetail>)orderDetailResult.Data;
        }

        private List<OrderDetail> GetPaginatedOrderDetails(int pageIndex, int pageSize)
        {
            var orderDetails = GetOrderDetails();
            TotalPages = (int)Math.Ceiling(orderDetails.Count / (double)pageSize);

            return orderDetails
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
        }

        private void SaveOrderDetail()
        {
            var orderDetailResult = _orderDetailBusiness.CreateOrderDetailAsync(this.OrderDetail).Result;

            if (orderDetailResult != null)
            {
                this.Message = orderDetailResult.Message;
                OrderDetails = GetOrderDetails();
                OrderDetails.Insert(0, this.OrderDetail);
            }
            else
            {
                this.Message = "Error system";
            }
        }

        private void DeleteOrderDetail(int id)
        {
            var orderDetailResult = _orderDetailBusiness.DeleteOrderDetailAsync(id).Result;

            if (orderDetailResult != null)
            {
                this.Message = orderDetailResult.Message;
            }
            else
            {
                this.Message = "Error system";
            }
        }
        private void UpdateOrderDetail()
        {
            var orderDetailResult = _orderDetailBusiness.UpdateOrderDetailAsync(this.OrderDetail).Result;

            if (orderDetailResult != null)
            {
                this.Message = orderDetailResult.Message;
            }
            else
            {
                this.Message = "Error system";
            }
        }

        private List<Event> GetEvents()
        {
            var eventResult = _eventBusiness.GetAllEvents();

            if (eventResult.Status > 0 && eventResult.Result.Data != null)
            {
                var currencies = (List<Event>)eventResult.Result.Data;
                return currencies;
            }
            return new List<Event>();
        }
        private List<Order> GetOrders()
        {
            var orderResult = _orderBusiness.GetAllOrders();

            if (orderResult.Status > 0 && orderResult.Result.Data != null)
            {
                var currencies = (List<Order>)orderResult.Result.Data;
                return currencies;
            }
            return new List<Order>();
        }
        public IActionResult OnPostSearch(string paymentMethod, decimal? price, DateTime? startDate, string nameEvent, int pageIndex = 1)
        {
            PageIndex = pageIndex;
            OrderDetails = GetFilteredOrderDetails(paymentMethod, price, startDate, nameEvent);
            TotalPages = (int)Math.Ceiling(OrderDetails.Count / (double)PageSize);
            OrderDetails = OrderDetails.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();

            Events = GetEvents();
            Orders = GetOrders();
            return Page();
        }

        private List<OrderDetail> GetFilteredOrderDetails(string paymentMethod, decimal? price, DateTime? startDate, string nameEvent)
        {
            var orderDetails = _orderDetailBusiness.GetAllOrderDetailsAsync().Result.Data as List<OrderDetail>;

            if (!string.IsNullOrEmpty(paymentMethod))
            {
                orderDetails = orderDetails.Where(od => od.Order.PaymentMethod == paymentMethod).ToList();
            }
            if (!string.IsNullOrEmpty(nameEvent))
            {
                orderDetails = orderDetails.Where(od => od.Event.Name == nameEvent).ToList();
            }
            if (price != null)
            {
                orderDetails = orderDetails.Where(od => od.Price == price).ToList();
            }
            if (startDate != null)
            {
                orderDetails = orderDetails.Where(od => od.Event.StartDate.Date == startDate.Value.Date).ToList();
            }

            return orderDetails;
        }
    }
}
