using System;
using System.Collections.Generic;
using System.Linq;
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
using FakturyPro.Data.Dto;
using FakturyPro.Klasy;

namespace FakturyPro
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

        public void RefreshOrders()
        {
            ZamowieniaTab.OrdersListBox.Items.Refresh();
        }

        public void RefreshInvoices()
        {
            FakturyTab.InvoicesListBox.Items.Refresh();
        }



        private void ExitApplication(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ZmienDaneFirmy_Click(object sender, RoutedEventArgs e)
        {
            ClientDto sprzedawca = (ClientDto)App.Current.Properties["Sprzedawca"];
            CustomerWindow win = new CustomerWindow(sprzedawca);
            if (win.ShowDialog() == true)
            {
                App.Current.Properties["Sprzedawca"] = win.Klient;
            }
        }
    }
}
