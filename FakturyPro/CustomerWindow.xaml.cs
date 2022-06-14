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
using FakturyPro.Klasy;

namespace FakturyPro
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        public CustomerWindow(Kontrahent kontrahent)
        {
            klient = new Kontrahent();
            if (kontrahent != null)
            {
                edytowany = kontrahent;
                klient.Nazwa = edytowany.Nazwa;
                klient.Miasto = edytowany.Miasto;
                klient.Adres = edytowany.Adres;
                klient.KodPocztowy = edytowany.KodPocztowy;
                klient.NIP = edytowany.NIP;
                klient.Telefon = edytowany.Telefon;
                klient.Email = edytowany.Email;
            }
            
            //klient = (kontrahent == null) ? new Kontrahent() : kontrahent;

            InitializeComponent();
        }

        private Kontrahent edytowany;

        private Kontrahent klient;

        public Kontrahent Klient
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
                edytowany.Nazwa = klient.Nazwa;
                edytowany.Miasto = klient.Miasto;
                edytowany.Adres = klient.Adres;
                edytowany.KodPocztowy = klient.KodPocztowy;
                edytowany.NIP = klient.NIP;
                edytowany.Telefon = klient.Telefon;
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
