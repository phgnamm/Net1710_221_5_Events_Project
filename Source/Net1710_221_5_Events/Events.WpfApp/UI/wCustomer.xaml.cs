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
    /// Interaction logic for wCustomer.xaml
    /// </summary>
    public partial class wCustomer : Window
    {
        private readonly CustomerBusiness _customerBusiness;

        public wCustomer()
        {
            InitializeComponent();
            this._customerBusiness ??= new CustomerBusiness();
            this.LoadGrdEvents();
        }

        private async void LoadGrdEvents()
        {
            var result = await _customerBusiness.GetAllCustomers();

            if (result.Status > 0 && result.Data != null)
            {
                grdCustomer.ItemsSource = result.Data as List<Customer>;
            }
            else
            {
                grdCustomer.ItemsSource = new List<Customer>();
            }
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var item = await _customerBusiness.GetCustomer(Int32.Parse(txtCustomerId.Text));

                if (item.Data == null)
                {
                    var customer = new Customer()
                    {
                        CustomerId = Int32.Parse(txtCustomerId.Text),
                        FullName = txtName.Text,
                        Email = txtEmail.Text,
                    };

                    var result = await _customerBusiness.Update(customer);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var customer = item.Data as Customer;
                    //currency.CurrencyCode = txtCurrencyCode.Text;
                    customer.FullName = txtName.Text;
                    customer.Email = txtEmail.Text;

                    var result = await _customerBusiness.Update(customer);
                    MessageBox.Show(result.Message, "Update");
                }

                txtCustomerId.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtName.Text = string.Empty;
                this.LoadGrdEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private async void grdCurrency_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Customer;
                    if (item != null)
                    {
                        var currencyResult = await _customerBusiness.GetCustomer(item.CustomerId);

                        if (currencyResult.Status > 0 && currencyResult.Data != null)
                        {
                            item = currencyResult.Data as Customer;
                            txtCustomerId.Text = item.CustomerId.ToString();
                            txtName.Text = item.FullName;
                            txtEmail.Text = item.Email;
                        }
                    }
                }
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void grdCurrency_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            string currencyCode = btn.CommandParameter.ToString();

            if (!string.IsNullOrEmpty(currencyCode))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _customerBusiness.Delete(Int32.Parse(currencyCode));
                    MessageBox.Show($"{result.Message}", "Delete");
                    this.LoadGrdEvents();
                }
            }
        }
    }
}

