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
            var result = await _eventBusiness.GetAllEvents();

            if (result.Status > 0 && result.Data != null)
            {
                grdEvent.ItemsSource = result.Data as List<Event>;
            }
            else
            {
                grdEvent.ItemsSource = new List<Event>();
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {

        }
        private void grdCurrency_MouseDouble_Click(object sender, RoutedEventArgs e) { }

        private void grdCurrency_ButtonDelete_Click(object sender, RoutedEventArgs e) { }
    }
}
