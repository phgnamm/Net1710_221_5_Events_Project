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
        private int? _currentOrderDetailId = null;
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
                    if (_currentOrderDetailId.HasValue)
                    {
                        // Cập nhật OrderDetail
                        var existingOrderDetail = new OrderDetail
                        {
                            OrderDetailId = _currentOrderDetailId.Value,
                            EventId = selectedEvent.EventId,
                            OrderId = selectedOrder.OrderId,
                            Quantity = quantity,
                            Price = price
                        };
                        var result = await _business.UpdateOrderDetailAsync(existingOrderDetail);
                        if (result.Status > 0)
                        {
                            LoadGrdOrderDetail();
                            MessageBox.Show(result.Message);
                        }
                        else
                        {
                            MessageBox.Show(result.Message);
                        }
                    }
                    else
                    {
                        // Tạo mới OrderDetail
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
                            MessageBox.Show(result.Message);
                        }
                        else
                        {
                            MessageBox.Show(result.Message);
                        }
                    }
                    // Reset form and state
                    ResetForm();
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
        private async void grdCurrency_ButtonDelete_Click(object sender, RoutedEventArgs e) 
        {
            Button btn = (Button)sender;
            string currencyCode = btn.CommandParameter.ToString();

            if (!string.IsNullOrEmpty(currencyCode))
            {
                var result = MessageBox.Show("Are you sure you want to delete this order detail?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    int currentparse =Int32.Parse(currencyCode);
                    var deleteResult = await _business.DeleteOrderDetailAsync(currentparse);
                    if (deleteResult.Status > 0)
                    {
                        this.LoadGrdOrderDetail();
                        MessageBox.Show(deleteResult.Message);
                    }
                    else
                    {
                        MessageBox.Show(deleteResult.Message);
                    }
                }
            }
        }
        private void grdCurrency_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrderDetail = grdCurrency.SelectedItem as OrderDetail;
            if (selectedOrderDetail != null)
            {
                var selectedEvent = cmbEventName.Items.OfType<Event>().FirstOrDefault(ev => ev.EventId == selectedOrderDetail.EventId);
                cmbEventName.SelectedItem = selectedEvent;

                var selectedOrder = cmbOrderCode.Items.OfType<Order>().FirstOrDefault(ord => ord.OrderId == selectedOrderDetail.OrderId);
                cmbOrderCode.SelectedItem = selectedOrder;

                txtQuantity.Text = selectedOrderDetail.Quantity.ToString();
                txtPrice.Text = selectedOrderDetail.Price.ToString();

                _currentOrderDetailId = selectedOrderDetail.OrderDetailId;
            }
        }
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
        private void ResetForm()
        {
            cmbEventName.SelectedItem = null;
            cmbOrderCode.SelectedItem = null;
            txtQuantity.Clear();
            txtPrice.Clear();
            _currentOrderDetailId = null;
        }


    }
}
