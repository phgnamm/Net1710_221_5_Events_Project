using Events.Business.Business;
using Events.Data.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
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
    /// Interaction logic for wCompany.xaml
    /// </summary>
    public partial class wCompany : Window
    {
        private readonly string _filePath;
        private readonly CompanyBusiness _companyBusiness;
        public ObservableCollection<Company> CompanyList { get; set; }
        public async Task<List<Company>> LoadCompaniesAsync()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Company>();
            }

            using (var reader = new StreamReader(_filePath))
            {
                var json = await reader.ReadToEndAsync();
                return JsonConvert.DeserializeObject<List<Company>>(json);
            }
        }

        public async Task SaveCompaniesAsync(List<Company> companies)
        {
            var json = JsonConvert.SerializeObject(companies, Formatting.Indented);
            using (var writer = new StreamWriter(_filePath))
            {
                await writer.WriteAsync(json);
            }
        }
        public wCompany()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            _filePath = configuration["jsonPath"];
            if (!File.Exists(_filePath))
            {
                MessageBox.Show("Đường dẫn file json không tồn tại. Vui lòng sửa trong appsetting.json.", "File Path Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
                return;
            }

            InitializeComponent();
            this._companyBusiness ??= new CompanyBusiness();
            CompanyList = new ObservableCollection<Company>();
            LoadCompanies();
            DataContext = this;
        }
        private async void LoadCompanies()
        {
            var companies = await LoadCompaniesAsync();
            foreach (var company in companies)
            {
                CompanyList.Add(company);
            }
        }

        private async void btnDeleteCompany_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var companyId = int.Parse(txtUpdateCompanyId.Text);
                var company = CompanyList.FirstOrDefault(c => c.CompanyId == companyId);

                if (company != null)
                {
                    CompanyList.Remove(company);
                    await SaveCompaniesAsync(CompanyList.ToList());
                    MessageBox.Show("Company deleted successfully.");
                    ClearInputFields();
                }
                else
                {
                    MessageBox.Show("Failed to delete company in the list.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async void btnUpdateCompany_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var companyId = int.Parse(txtUpdateCompanyId.Text);
                var existingCompany = CompanyList.FirstOrDefault(c => c.CompanyId == companyId);

                if (existingCompany != null)
                {
                    existingCompany.Name = txtUpdateName.Text;
                    existingCompany.CompanyPhone = txtUpdateCompanyPhone.Text;
                    existingCompany.BusinessSector = txtUpdateBusinessSector.Text;
                    existingCompany.TaxesId = txtUpdateTaxesId.Text;
                    existingCompany.Address = txtUpdateAddress.Text;
                    existingCompany.City = txtUpdateCity.Text;
                    existingCompany.Country = txtUpdateCountry.Text;
                    existingCompany.CreatedDate = DateTime.Parse(txtUpdateCreatedAt.Text, CultureInfo.InvariantCulture);
                    existingCompany.UpdatedDate = DateTime.Parse(txtUpdateUpdatedAt.Text, CultureInfo.InvariantCulture);

                    existingCompany.UpdatedDate = DateTime.Now;
                    await SaveCompaniesAsync(CompanyList.ToList());

                    int index = CompanyList.IndexOf(CompanyList.FirstOrDefault(c => c.CompanyId == companyId));
                    if (index != -1)
                    {
                        CompanyList.RemoveAt(index);
                        CompanyList.Insert(index, existingCompany);
                        selectedItem(existingCompany);
                        MessageBox.Show("Company updated successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Failed to update company in the list.");
                    }
                }
                else
                {
                    MessageBox.Show("Failed to update company.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void grdCompany_MouseDouble_Click(object sender, MouseButtonEventArgs e)
        {
            var selectedCompany = grdCompany.SelectedItem as Company;
            if (selectedCompany != null)
            {
                foreach (TabItem tabItem in tabControlCompanies.Items)
                {
                    if (tabItem.Header.ToString() == "Details")
                    {
                        tabControlCompanies.SelectedItem = tabItem;
                        break;
                    }
                }

                selectedItem(selectedCompany);
            }
        }

        private void selectedItem(Company selectedCompany)
        {
            txtUpdateCompanyId.Text = selectedCompany.CompanyId.ToString();
            txtUpdateName.Text = selectedCompany.Name;
            txtUpdateCompanyPhone.Text = selectedCompany.CompanyPhone;
            txtUpdateBusinessSector.Text = selectedCompany.BusinessSector;
            txtUpdateTaxesId.Text = selectedCompany.TaxesId;
            txtUpdateAddress.Text = selectedCompany.Address;
            txtUpdateCity.Text = selectedCompany.City;
            txtUpdateCountry.Text = selectedCompany.Country;
            txtUpdateCreatedAt.Text = selectedCompany.CreatedDate.ToString();
            txtUpdateUpdatedAt.Text = selectedCompany.UpdatedDate.ToString();
        }

        private async void btnAddCompany_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newCompany = new Company
                {
                    CompanyId = CompanyList.Count > 0 ? CompanyList.Max(c => c.CompanyId) + 1 : 1,
                    Name = txtAddName.Text,
                    CompanyPhone = txtAddCompanyPhone.Text,
                    BusinessSector = txtAddBusinessSector.Text,
                    TaxesId = txtAddTaxesId.Text,
                    Address = txtAddAddress.Text,
                    City = txtAddCity.Text,
                    Country = txtAddCountry.Text,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                };

                CompanyList.Add(newCompany);
                await SaveCompaniesAsync(CompanyList.ToList());
                MessageBox.Show("Company added successfully.");
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void ClearInputFields()
        {
            txtAddName.Text = "";
            txtAddCompanyPhone.Text = "";
            txtAddBusinessSector.Text = "";
            txtAddTaxesId.Text = "";
            txtAddAddress.Text = "";
            txtAddCity.Text = "";
            txtAddCountry.Text = "";

            txtUpdateCompanyId.Text = "";
            txtUpdateName.Text = "";
            txtUpdateCompanyPhone.Text = "";
            txtUpdateBusinessSector.Text = "";
            txtUpdateTaxesId.Text = "";
            txtUpdateAddress.Text = "";
            txtUpdateCity.Text = "";
            txtUpdateCountry.Text = "";
            txtUpdateCreatedAt.Text = "";
            txtUpdateUpdatedAt.Text = "";
        
        }
    /*  private async void LoadCompanies()
      {
          var result = await _companyBusiness.GetAll();

          if (result.Status > 0 && result.Data != null)
          {
              foreach (var company in result.Data as List<Company>)
              {
                  CompanyList.Add(company);
              }
          }
      }
      private async void btnDeleteCompany_Click(object sender, RoutedEventArgs e)
      {
          try
          {

              var result = await _companyBusiness.Delete(int.Parse(txtUpdateCompanyId.Text));

              if (result.Status > 0)
              {
                  int index = CompanyList.IndexOf(CompanyList.FirstOrDefault(c => c.CompanyId == int.Parse(txtUpdateCompanyId.Text)));
                  if (index != -1)
                  {
                      CompanyList.RemoveAt(index);
                      MessageBox.Show("Company deleted successfully.");
                      ClearInputFields();
                  }
                  else
                  {
                      MessageBox.Show("Failed to delete company in the list.");
                  }
              }
              else
              {
                  MessageBox.Show("Failed to delete company.");
              }
          }
          catch (Exception ex)
          {
              MessageBox.Show($"Error: {ex.Message}");
          }
      }
      private async void btnUpdateCompany_Click(object sender, RoutedEventArgs e)
      {
          try
          {
              Company updatedCompany = new Company
              {
                  CompanyId = int.Parse(txtUpdateCompanyId.Text),
                  Name = txtUpdateName.Text,
                  CompanyPhone = txtUpdateCompanyPhone.Text,
                  BusinessSector = txtUpdateBusinessSector.Text,
                  TaxesId = txtUpdateTaxesId.Text,
                  Address = txtUpdateAddress.Text,
                  City = txtUpdateCity.Text,
                  Country = txtUpdateCountry.Text,
                  CreatedDate = DateTime.Parse(txtUpdateCreatedAt.Text, CultureInfo.InvariantCulture),
                  UpdatedDate = DateTime.Parse(txtUpdateUpdatedAt.Text, CultureInfo.InvariantCulture)
              };

              // Assuming CompanyBusiness has a method to add a new company
              var result = await _companyBusiness.Update(updatedCompany);

              if (result.Status > 0)
              {
                  int index = CompanyList.IndexOf(CompanyList.FirstOrDefault(c => c.CompanyId == updatedCompany.CompanyId));
                  if (index != -1)
                  {
                      CompanyList.RemoveAt(index);
                      CompanyList.Insert(index, result.Data as Company);
                      selectedItem(result.Data as Company);
                      MessageBox.Show("Company updated successfully.");
                  }
                  else
                  {
                      MessageBox.Show("Failed to update company in the list.");
                  }
              }
              else
              {
                  MessageBox.Show("Failed to update company.");
              }
          }
          catch (Exception ex)
          {
              MessageBox.Show($"Error: {ex.Message}");
          }
      }

      private void grdCompany_MouseDouble_Click(object sender, MouseButtonEventArgs e)
      {
          var selectedCompany = grdCompany.SelectedItem as Company;
          if (selectedCompany != null)
          {
              // Switch to Details tab
              foreach (TabItem tabItem in tabControlCompanies.Items)
              {
                  if (tabItem.Header.ToString() == "Details")
                  {
                      tabControlCompanies.SelectedItem = tabItem;
                      break;
                  }
              }

              // Populate details in TextBoxes
              selectedItem(selectedCompany);
          }
      }
      private void selectedItem(Company selectedCompany)
      {
          txtUpdateCompanyId.Text = selectedCompany.CompanyId.ToString();
          txtUpdateName.Text = selectedCompany.Name;
          txtUpdateCompanyPhone.Text = selectedCompany.CompanyPhone;
          txtUpdateBusinessSector.Text = selectedCompany.BusinessSector;
          txtUpdateTaxesId.Text = selectedCompany.TaxesId;
          txtUpdateAddress.Text = selectedCompany.Address;
          txtUpdateCity.Text = selectedCompany.City;
          txtUpdateCountry.Text = selectedCompany.Country;
          txtUpdateCreatedAt.Text = selectedCompany.CreatedDate.ToString();
          txtUpdateUpdatedAt.Text = selectedCompany.UpdatedDate.ToString();
      }
      private async void btnAddCompany_Click(object sender, RoutedEventArgs e)
      {
          try
          {
              Company newCompany = new Company
              {
                  Name = txtAddName.Text,
                  CompanyPhone = txtAddCompanyPhone.Text,
                  BusinessSector = txtAddBusinessSector.Text,
                  TaxesId = txtAddTaxesId.Text,
                  Address = txtAddAddress.Text,
                  City = txtAddCity.Text,
                  Country = txtAddCountry.Text
              };

              // Assuming CompanyBusiness has a method to add a new company
              var result = await _companyBusiness.Create(newCompany);

              if (result.Status > 0)
              {
                  CompanyList.Add(result.Data as Company);
                  MessageBox.Show("Company added successfully.");
                  ClearInputFields();
              }
              else
              {
                  MessageBox.Show("Failed to add company.");
              }
          }
          catch (Exception ex)
          {
              MessageBox.Show($"Error: {ex.Message}");
          }
      }
      private void ClearInputFields()
      {
          // Clear all text boxes
          txtAddName.Text = "";
          txtAddCompanyPhone.Text = "";
          txtAddBusinessSector.Text = "";
          txtAddTaxesId.Text = "";
          txtAddAddress.Text = "";
          txtAddCity.Text = "";
          txtAddCountry.Text = "";

          txtUpdateCompanyId.Text = "";
          txtUpdateName.Text = "";
          txtUpdateCompanyPhone.Text = "";
          txtUpdateBusinessSector.Text = "";
          txtUpdateTaxesId.Text = "";
          txtUpdateAddress.Text = "";
          txtUpdateCity.Text = "";
          txtUpdateCountry.Text = "";
          txtUpdateCreatedAt.Text = "";
          txtUpdateUpdatedAt.Text = "";

      }*/
    }
}
