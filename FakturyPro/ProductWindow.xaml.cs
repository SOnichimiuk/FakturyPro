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
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        public ProductWindow(TowarMagazyn edytowany)
        {
            towar = edytowany == null ? new TowarMagazyn() : new TowarMagazyn(edytowany);

            this.edytowany = edytowany;

            InitializeComponent();
        }

        private TowarMagazyn edytowany;

        private TowarMagazyn towar;

        public TowarMagazyn Towar
        {
            get { return towar; }
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
                edytowany.Nazwa = towar.Nazwa;
                edytowany.Ilosc = towar.Ilosc;
                edytowany.Jednostka = towar.Jednostka;
                edytowany.CenaNetto = towar.CenaNetto;
                edytowany.Vat = towar.Vat;
                towar = edytowany;
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
