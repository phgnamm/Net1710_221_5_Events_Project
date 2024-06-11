using Events.Business;
using Events.Data.Models;
using Microsoft.IdentityModel.Tokens;
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
using System.Xml.Linq;

namespace Events.WpfApp.UI
{
    /// <summary>
    /// Interaction logic for wOrder.xaml
    /// </summary>
    public partial class wOrder : Window
    {
        private readonly OrderBusiness _orderBusiness;
        public wOrder()
        {
            InitializeComponent();
            this._orderBusiness ??= new OrderBusiness();
            this.LoadGrdEvents();
        }

        private async void LoadGrdEvents()
        {
            ClearTextFields();
            var result = await _orderBusiness.GetAllOrders();

            ButtonAdd.IsEnabled = true;
            ButtonSave.IsEnabled = false;

            if (result.Status > 0 && result.Data != null)
            {
                grdOrder.ItemsSource = result.Data as List<Order>;
            }
            else
            {
                grdOrder.ItemsSource = new List<Order>();
            }
        }

        private void ClearTextFields()
        {
            txtId.Text = string.Empty;
            txtCode.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtTicketQuantity.Text = string.Empty;
            txtTotalAmount.Text = string.Empty;
            txtPaymentDate.Text = string.Empty;
            txtStatus.Text = string.Empty;
            txtPaymentStatus.Text = string.Empty;
            txtPaymentMethod.Text = string.Empty;
            txtCustomerId.Text = string.Empty;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            ClearTextFields();
            ButtonAdd.IsEnabled = false;
            ButtonSave.IsEnabled = true;
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int orderId = 0;
                if (!txtId.Text.IsNullOrEmpty())
                {
                    orderId = int.Parse(txtId.Text);
                }
                var item = await _orderBusiness.GetOrderById(orderId);

                if (item.Data == null)
                {
                    var orderItem = new Order()
                    {
                        Code = txtCode.Text,
                        Description = txtDescription.Text,
                        TicketQuantity = int.Parse(txtTicketQuantity.Text),
                        TotalAmount = int.Parse(txtTotalAmount.Text),
                        PaymentStatus = txtPaymentStatus.Text,
                        PaymentMethod = txtPaymentMethod.Text,
                        Status = txtStatus.Text,
                        PaymentDate = DateTime.Parse(txtPaymentDate.Text),
                        CustomerId = int.Parse(txtCustomerId.Text),
                    };

                    var result = await _orderBusiness.CreateNewOrder(orderItem);
                    MessageBox.Show(result.Message, "Save", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    var orderItem = item.Data as Order;

                    orderItem.Code = txtCode.Text;
                    orderItem.Description = txtDescription.Text;
                    orderItem.TicketQuantity = int.Parse(txtTicketQuantity.Text);
                    orderItem.TotalAmount = int.Parse(txtTotalAmount.Text);
                    orderItem.PaymentStatus = txtPaymentStatus.Text;
                    orderItem.PaymentMethod = txtPaymentMethod.Text;
                    orderItem.Status = txtStatus.Text;
                    orderItem.PaymentDate = DateTime.Parse(txtPaymentDate.Text);
                    orderItem.CustomerId = int.Parse(txtCustomerId.Text);

                    var result = await _orderBusiness.UpdateOrderById(orderItem);
                    MessageBox.Show(result.Message, "Update", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                this.LoadGrdEvents();
                ButtonAdd.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.LoadGrdEvents();
            }
        }

        private async void grdOrder_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ButtonSave.IsEnabled = true;
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItem != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Order;
                    if (item != null)
                    {
                        var eventResult = await _orderBusiness.GetOrderById(item.OrderId);

                        if (eventResult.Status > 0 && eventResult.Data != null)
                        {
                            item = eventResult.Data as Order;
                            txtId.Text = item.OrderId.ToString();
                            txtCode.Text = item.Code.ToString();
                            txtDescription.Text = item.Description.ToString();
                            txtPaymentMethod.Text = item.PaymentMethod.ToString();
                            txtPaymentStatus.Text = item.PaymentStatus.ToString();
                            txtTicketQuantity.Text = item.TicketQuantity.ToString();
                            txtStatus.Text = item.Status.ToString();
                            txtPaymentDate.Text = item.PaymentDate.ToString();
                            txtCustomerId.Text = item.CustomerId.ToString();
                            txtTotalAmount.Text = item.TotalAmount.ToString();
                        }
                    }
                }
            }
        }

        private async void grdOrder_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            int orderId = int.Parse(btn.CommandParameter.ToString());

            if (orderId > 0)
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _orderBusiness.DeleteOrderById(orderId);
                    MessageBox.Show($"{result.Message}", "Delete", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.LoadGrdEvents();
                }
            }
        }
    }
}
