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

        public string Message { get; set; } = default;
        [BindProperty]
        public OrderDetail OrderDetail { get; set; } = default;
        public List<OrderDetailDto> OrderDetails { get; set; } = new List<OrderDetailDto>();

        public void OnGet()
        {
            OrderDetails = GetOrderDetails();
        }

        public IActionResult OnPost()
        {
            SaveOrderDetail();
            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int id)
        {
            DeleteOrderDetail(id);
            return RedirectToPage();
        }

        private List<OrderDetailDto> GetOrderDetails()
        {
            var orderDetailResult = _orderDetailBusiness.GetAllOrderDetailsAsync().Result;

            if (orderDetailResult.Status == Const.SUCCESS_READ_CODE && orderDetailResult.Data != null)
            {
                var orderDetails = (List<OrderDetailDto>)orderDetailResult.Data;
                return orderDetails;
            }
            return new List<OrderDetailDto>();
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
    }
}
