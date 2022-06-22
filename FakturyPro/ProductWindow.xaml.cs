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
using FakturyPro.Data.Models;
using FakturyPro.Klasy;
using FakturyPro.Services;

namespace FakturyPro
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        public ProductWindow(ProductDto edytowany)
        {
            towar = edytowany != null ? edytowany : new ProductDto();

            this.edytowany = edytowany;

            int vat = (int)edytowany.VatRate;
            PriceBrutto = edytowany != null ? edytowany.PriceNetto * vat / 100 : 0;

            InitializeComponent();
        }

        private ProductDto edytowany;

        private ProductDto towar;

        public double PriceBrutto;
       
        public ProductDto Towar
        {
            get { return towar; }
        }
        

        private void Save(object sender, ExecutedRoutedEventArgs e)
        {

            if (edytowany != null)
            {
                towar.Name = edytowany.Name;
                towar.Quantity = edytowany.Quantity;
                towar.PriceNetto = edytowany.PriceNetto;
                towar.VatRate = edytowany.VatRate;
            }
           
            DialogResult = true;
            Close();
        }

        private void Cancel(object sender, ExecutedRoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

    }
}
