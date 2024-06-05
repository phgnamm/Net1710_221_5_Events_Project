﻿using Events.Business.Business;
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
        public wOrderDetail()
        {
            this._business ??= new OrderDetailBusiness();
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
        private void ButtonSave_Click(object sender, RoutedEventArgs e) { }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e) { }
        private void grdCurrency_ButtonDelete_Click(object sender, RoutedEventArgs e) { }
        private void grdCurrency_MouseDouble_Click(object sender, RoutedEventArgs e) { }
    }
}
