﻿using Events.WpfApp.UI;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace Events.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Open_wOrderDetail_Click(object sender, RoutedEventArgs e)
        {
            var p= new wOrderDetail();
            p.Owner = this;
            p.Show();
        }
        private void Open_wEvent_Click(object sender, RoutedEventArgs e)
        {
            var p = new wEvent();
            p.Owner = this;
            p.Show();
        }

        private void Open_wCustomer_Click(object sender, RoutedEventArgs e)
        {
            var p = new wCustomer();
            p.Owner = this;
            p.Show();
        }
        private void Open_wOrder_Click(object sender, RoutedEventArgs e)
        {
            var p = new wOrder();
            p.Owner = this;
            p.Show();
        }
    }
}