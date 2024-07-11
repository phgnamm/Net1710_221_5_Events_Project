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
    /// Interaction logic for wEvent.xaml
    /// </summary>
    public partial class wEvent : Window
    {
        private readonly EventBusiness _eventBusiness;
        public wEvent()
        {
            InitializeComponent();
            this._eventBusiness ??= new EventBusiness();
            this.LoadGrdEvents();
        }
        private async void LoadGrdEvents()
        {
            ClearTextFields();
            var result = await _eventBusiness.GetAllEvents();

            ButtonAdd.IsEnabled = true;
            ButtonSave.IsEnabled = false;

            if (result.Status > 0 && result.Data != null)
            {
                grdEvent.ItemsSource = result.Data as List<Event>;
            }
            else
            {
                grdEvent.ItemsSource = new List<Event>();
            }
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int eventId = 0;
                if (!txtId.Text.IsNullOrEmpty())
                {
                    eventId = int.Parse(txtId.Text);
                }
                var item = await _eventBusiness.GetEventById(eventId);

                if (item.Data == null)
                {
                    var eventItem = new Event()
                    {
                        ImageLink = txtImage.Text,
                        Name = txtName.Text,
                        Location = txtLocation.Text,
                        Description = txtDescription.Text,
                        StartDate = DateTime.Parse(txtStartDate.Text),
                        EndDate = DateTime.Parse(txtEndDate.Text),
                        OpenTicket = DateTime.Parse(txtOpenTicket.Text),
                        CloseTicket = DateTime.Parse(txtCloseTicket.Text),
                        TicketPrice = int.Parse(txtTicketPrice.Text),
                        Quantity = int.Parse(txtQuantity.Text),
                        OperatorName = txtOperator.Text,
             //           IsDelete = false
                    };

                    var result = await _eventBusiness.CreateNewEvent(eventItem);
                    MessageBox.Show(result.Message, "Save", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    var eventItem = item.Data as Event;

                    eventItem.ImageLink = txtImage.Text;
                    eventItem.Name = txtName.Text;
                    eventItem.Location = txtLocation.Text;
                    eventItem.Description = txtDescription.Text;
                    eventItem.StartDate = DateTime.Parse(txtStartDate.Text);
                    eventItem.EndDate = DateTime.Parse(txtEndDate.Text);
                    eventItem.OpenTicket = DateTime.Parse(txtOpenTicket.Text);
                    eventItem.CloseTicket = DateTime.Parse(txtCloseTicket.Text);
                    eventItem.TicketPrice = int.Parse(txtTicketPrice.Text);
                    eventItem.Quantity = int.Parse(txtQuantity.Text);
                    eventItem.OperatorName = txtOperator.Text;
            //        eventItem.IsDelete = false;

                    var result = await _eventBusiness.UpdateEventById(eventItem);
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

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void grdEvent_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ButtonSave.IsEnabled = true;
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItem != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Event;
                    if (item != null)
                    {
                        var eventResult = await _eventBusiness.GetEventById(item.EventId);

                        if (eventResult.Status > 0 && eventResult.Data != null)
                        {
                            item = eventResult.Data as Event;
                            txtId.Text = item.EventId.ToString();
                            txtImage.Text = item.ImageLink;
                            txtName.Text = item.Name;
                            txtDescription.Text = item.Description;
                            txtLocation.Text = item.Location;
                            txtStartDate.Text = item.StartDate.ToString();
                            txtEndDate.Text = item.EndDate.ToString();
                            txtOpenTicket.Text = item.OpenTicket.ToString();
                            txtCloseTicket.Text = item.CloseTicket.ToString();
                            txtOperator.Text = item.OperatorName;
                            txtTicketPrice.Text = item.TicketPrice.ToString();
                            txtQuantity.Text = item.Quantity.ToString();
                        }
                    }
                }
            }
        }

        private async void grdEvent_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            int eventId = int.Parse(btn.CommandParameter.ToString());

            if (eventId > 0)
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _eventBusiness.DeleteEventById(eventId);
                    MessageBox.Show($"{result.Message}", "Delete", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.LoadGrdEvents();
                }
            }
        }

        private void ClearTextFields()
        {
            txtId.Text = string.Empty;
            txtImage.Text = string.Empty;
            txtName.Text = string.Empty;
            txtLocation.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtImage.Text = string.Empty;
            txtStartDate.Text = string.Empty;
            txtEndDate.Text = string.Empty;
            txtOpenTicket.Text = string.Empty;
            txtCloseTicket.Text = string.Empty;
            txtTicketPrice.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtOperator.Text = string.Empty;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            ClearTextFields();
            ButtonAdd.IsEnabled = false;
            ButtonSave.IsEnabled = true;
        }
    }
}
