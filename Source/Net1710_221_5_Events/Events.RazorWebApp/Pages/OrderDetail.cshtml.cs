using Events.Business;
using Events.Business.Business;
using Events.Common;
using Events.Data.DTOs;
using Events.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

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

        public void OnGet()
        {
            OrderDetails = GetOrderDetails();
            Events = GetEvents();
            Orders = GetOrders();
        }

        public IActionResult OnPost()
        {
            SaveOrderDetail();
            return RedirectToPage();
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

        private void SaveOrderDetail()
        {
            var orderDetailResult = _orderDetailBusiness.CreateOrderDetailAsync(this.OrderDetail).Result;

            if (orderDetailResult != null)
            {
                this.Message = orderDetailResult.Message;
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

        //getevent
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
    }
}
