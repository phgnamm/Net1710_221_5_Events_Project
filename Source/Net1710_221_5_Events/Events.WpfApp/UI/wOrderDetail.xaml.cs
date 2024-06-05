using Events.Business;
using Events.Business.Business;
using Events.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Events.WpfApp.UI
{
    /// <summary>
    /// Interaction logic for wOrderDetail.xaml
    /// </summary>
    public partial class wOrderDetail : Window
    {
        private readonly OrderDetailBusiness _business;
        private readonly EventBusiness _eventBusiness;
        private readonly OrderBusiness _orderBusiness;
        public wOrderDetail()
        {
            this._business ??= new OrderDetailBusiness();
            _eventBusiness = new EventBusiness();
            _orderBusiness = new OrderBusiness();
            this.LoadComboBoxes();
            this.LoadGrdOrderDetail();
            InitializeComponent();
        }
        private async void LoadGrdOrderDetail()
        {
            var result = await _business.GetAllOrderDetailsAsync();

            if (result.Status > 0 && result.Data != null)
            {
                grdCurrency.ItemsSource = result.Data as List<OrderDetail>;
            }
            else
            {
                grdCurrency.ItemsSource = new List<OrderDetail>();
            }
        }
        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            var selectedEvent = cmbEventName.SelectedItem as Event;
            var selectedOrder = cmbOrderCode.SelectedItem as Order;

            if (selectedEvent != null && selectedOrder != null)
            {
                if (int.TryParse(txtQuantity.Text, out int quantity) && decimal.TryParse(txtPrice.Text, out decimal price))
                {
                    var newOrderDetail = new OrderDetail
                    {
                        EventId = selectedEvent.EventId,
                        OrderId = selectedOrder.OrderId,
                        Quantity = quantity,
                        Price = price
                    };

                    var result = await _business.CreateOrderDetailAsync(newOrderDetail);
                    if (result.Status > 0)
                    {
                        LoadGrdOrderDetail();
                    }
                    else
                    {
                        MessageBox.Show("Failed to save Order Detail");
                    }
                }
                else
                {
                    MessageBox.Show("Please enter valid Quantity and Price values.");
                }
            }
            else
            {
                MessageBox.Show("Please select a valid Event and Order.");
            }
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e) 
        {
            Close();
        }
        private void grdCurrency_ButtonDelete_Click(object sender, RoutedEventArgs e) { }
        private void grdCurrency_MouseDouble_Click(object sender, RoutedEventArgs e) { }
        private async void LoadComboBoxes()
        {
            var eventResult = await _eventBusiness.GetAllEvents();
            if (eventResult.Status > 0 && eventResult.Data != null)
            {
                cmbEventName.ItemsSource = (List<Event>)eventResult.Data;
            }

            var orderResult = await _orderBusiness.GetAllOrders();
            if (orderResult.Status > 0 && orderResult.Data != null)
            {
                cmbOrderCode.ItemsSource = (List<Order>)orderResult.Data;
            }
        }

    }
}
