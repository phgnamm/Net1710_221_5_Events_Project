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
using System.Xml.Linq;

namespace Events.WpfApp.UI
{
    /// <summary>
    /// Interaction logic for wTicket.xaml
    /// </summary>
    public partial class wTicket : Window
    {
        private readonly TicketBusiness _ticketBusiness;
        public wTicket()
        {
            InitializeComponent();
            _ticketBusiness = new TicketBusiness();
            ClearTextFields();
            ButtonSave.IsEnabled = false;
            LoadData();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            ClearTextFields();
            ButtonAdd.IsEnabled = false;
            ButtonSave.IsEnabled = true;
        }


        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int ticketId = 0;
                if (!txtId.Text.IsNullOrEmpty())
                {
                    ticketId = int.Parse(txtId.Text);
                }
                var item = await _ticketBusiness.GetTicketByIdAsync(ticketId);

                if (item.Data == null)
                {
                    var ticket = new Ticket()
                    {
                        Code = txtCode.Text,
                        Qrcode = txtQrCode.Text,
                        ParticipantMail = txtParticipantMail.Text,
                        ParticipantName = txtParticipantName.Text,
                        ParticipantPhone = txtParticipantPhone.Text,
                        SpecialNote = txtSpecialNote.Text,
                        TicketType = txtTicketType.Text,
                        CreatedDate = DateTime.Parse(txtCreateDate.Text),

                    };

                    var result = await _ticketBusiness.CreateTicketAsync(ticket);
                    MessageBox.Show(result.Message, "Save", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    var ticket = item.Data as Ticket;

                    ticket.Code = txtCode.Text;
                    ticket.Qrcode = txtQrCode.Text;
                    ticket.ParticipantMail = txtParticipantMail.Text;
                    ticket.ParticipantPhone = txtParticipantPhone.Text;
                    ticket.ParticipantName = txtParticipantName.Text;
                    ticket.CreatedDate = DateTime.Parse(txtCreateDate.Text);
                    ticket.SpecialNote = txtSpecialNote.Text;
                    ticket.TicketType = txtTicketType.Text;
                    ticket.Status = txtTicketStatus.Text;

                    var result = await _ticketBusiness.UpdateTicketAsync(ticket);
                    MessageBox.Show(result.Message, "Update", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                this.LoadData();
                ButtonAdd.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.LoadData();
            }

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private async void grdTicket_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            int ticketId = int.Parse(btn.CommandParameter.ToString());

            if (ticketId > 0)
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _ticketBusiness.DeleteTicketAsync(ticketId);
                    MessageBox.Show($"{result.Message}", "Delete", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.LoadData();
                }
            }

        }
        private void grdTicket_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            ButtonSave.IsEnabled = true;
            ButtonAdd.IsEnabled = false;
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItem != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Ticket;
                    if (item != null)
                    {
                        txtId.Text = item.TicketId.ToString();
                        txtCode.Text = item.Code.ToString();
                        txtQrCode.Text = item.Qrcode.ToString();
                        txtParticipantMail.Text = item.ParticipantMail.ToString();
                        txtParticipantName.Text = item.ParticipantName.ToString();
                        txtParticipantPhone.Text = item.ParticipantPhone.ToString();
                        txtSpecialNote.Text = item.SpecialNote.ToString();
                        txtTicketType.Text = item.TicketType.ToString();
                        txtTicketStatus.Text = item.Status.ToString();
                        txtCreateDate.Text = item.CreatedDate.ToString();
                    }
                }
            }

        }
        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private async void LoadData()
        {
            List<Ticket> Tickets = new List<Ticket>();
            try
            {
                var result = await _ticketBusiness.GetAllTicketsAsync();
                Tickets = (List<Ticket>)result.Data;
                grdTicket.ItemsSource = Tickets;
            }
            catch (Exception ex)
            {
                grdTicket.ItemsSource = new List<Ticket>();
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearTextFields()
        {
            txtId.Text = string.Empty;
            txtCode.Text = string.Empty;
            txtQrCode.Text = string.Empty;
            txtParticipantName.Text = string.Empty;
            txtParticipantPhone.Text = string.Empty;
            txtParticipantMail.Text = string.Empty;
            txtTicketType.Text = string.Empty;
            txtSpecialNote.Text = string.Empty;
            txtTicketStatus.Text = string.Empty;

        }
    }
}
