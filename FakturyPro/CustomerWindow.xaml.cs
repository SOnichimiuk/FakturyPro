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
using System.Windows.Shapes;
using FakturyPro.Data.Dto;
using FakturyPro.Klasy;

namespace FakturyPro
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        public CustomerWindow(ClientDto kontrahent)
        {
            klient = new ClientDto();
            if (kontrahent != null)
            {
                edytowany = kontrahent;
                klient.Name = edytowany.Name;
                klient.City = edytowany.City;
                klient.Adress = edytowany.Adress;
                klient.PostalCode = edytowany.PostalCode;
                klient.NIP = edytowany.NIP;
                klient.PhoneNumber = edytowany.PhoneNumber;
                klient.Email = edytowany.Email;
            }
            
            //klient = (kontrahent == null) ? new Kontrahent() : kontrahent;

            InitializeComponent();
        }

        private ClientDto edytowany;

        private ClientDto klient;

        public ClientDto Klient
        {
            get { return klient; }
        }

        private void Save(object sender, ExecutedRoutedEventArgs e)
        {
            if (!IsValid(this))
            {
                MessageBox.Show("Formularz zawiera błędy. Popraw je.", "Błędy");
                return;
            }

            if (edytowany != null)
            {
                edytowany.Name = klient.Name;
                edytowany.City = klient.City;
                edytowany.Adress = klient.Adress;
                edytowany.PostalCode = klient.PostalCode;
                edytowany.NIP = klient.NIP;
                edytowany.PhoneNumber = klient.PhoneNumber;
                edytowany.Email = klient.Email;
                klient = edytowany;
            }
            DialogResult = true;
            Close();
        }

        private void Cancel(object sender, ExecutedRoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        public bool IsValid(DependencyObject parent)
        {
            if (Validation.GetHasError(parent))
                return false;

            // Validate all the bindings on the children
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); ++i)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (!IsValid(child)) { return false; }
            }

            return true;
        }
    }
}
