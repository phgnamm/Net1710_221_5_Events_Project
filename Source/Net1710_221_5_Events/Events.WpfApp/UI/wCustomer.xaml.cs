using Events.Business.Business;
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
            this.LoadGrdCustomers();
        }

        private async void LoadGrdCustomers()
        {
            ClearTextFields();
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
                int customerId = 0;
                if (!txtId.Text.IsNullOrEmpty())
                {
                    customerId = int.Parse(txtId.Text);
                }
                var item = await _customerBusiness.GetCustomer(customerId);

                if (item.Data == null)
                {
                    DateTime dateOfBirth;
                    if (DateTime.TryParse(txtDateOfBirth.Text, out dateOfBirth))
                    {
                        var customer = new Customer()
                        {
                            FullName = txtName.Text,
                            Email = txtEmail.Text,
                            PhoneNumber = txtPhone.Text,
                            Gender = txtGender.Text,
                            DateOfBirth = dateOfBirth,
                            Address = txtAddress.Text,
                            City = txtCity.Text,
                            Country = txtCountry.Text,
                            CreateDate = DateTime.Now
                        };
                        var result = await _customerBusiness.Create(customer);
                        MessageBox.Show(result.Message, "Save");
                    }
                }
                else
                {
                    var customer = item.Data as Customer;

                    customer.FullName = txtName.Text;
                    customer.Email = txtEmail.Text;
                    customer.PhoneNumber = txtPhone.Text;
                    customer.Gender = txtGender.Text;
                    DateTime dateOfBirth;
                    if (DateTime.TryParse(txtDateOfBirth.Text, out dateOfBirth))
                    {
                        customer.DateOfBirth = dateOfBirth;
                    }
                    else
                    {
                        customer.DateOfBirth = customer.DateOfBirth;
                    }
                    customer.Address = txtAddress.Text;
                    customer.City = txtCity.Text;
                    customer.Country = txtCountry.Text;
                    customer.CreateDate = customer.CreateDate;

                    var result = await _customerBusiness.Update(customer);
                    MessageBox.Show(result.Message, "Update", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                this.LoadGrdCustomers();
                ButtonAdd.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
                this.LoadGrdCustomers();
            }
        }

        private async void grdCustomer_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            ButtonSave.IsEnabled = true;
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Customer;
                    if (item != null)
                    {
                        var customerResult = await _customerBusiness.GetCustomer(item.CustomerId);

                        if (customerResult.Status > 0 && customerResult.Data != null)
                        {
                            item = customerResult.Data as Customer;
                            txtId.Text = item.CustomerId.ToString();
                            txtPhone.Text = item.PhoneNumber;
                            txtName.Text = item.FullName;
                            txtEmail.Text = item.Email;
                            txtGender.Text = item.Gender;
                            txtDateOfBirth.Text = item.DateOfBirth.ToString();
                            txtAddress.Text = item.Address;
                            txtCity.Text = item.City;
                            txtCountry.Text = item.Country;
                        }
                    }
                }
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void grdCustomer_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            string currencyCode = btn.CommandParameter.ToString();

            if (!string.IsNullOrEmpty(currencyCode))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _customerBusiness.Delete(Int32.Parse(currencyCode));
                    MessageBox.Show($"{result.Message}", "Delete");
                    this.LoadGrdCustomers();
                }
            }
        }

        private void ClearTextFields()
        {
            txtId.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtDateOfBirth.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtGender.Text = string.Empty;
            txtCountry.Text = string.Empty;
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            ClearTextFields();
            ButtonAdd.IsEnabled = false;
            ButtonSave.IsEnabled = true;
        }
    }
}