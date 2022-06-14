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
using FakturyPro.Klasy;
using Microsoft.Win32;
using FakturyPro.Konwertery;
using System.ComponentModel;

namespace FakturyPro.Szablony
{
    /// <summary>
    /// Interaction logic for Zamowienia.xaml
    /// </summary>
    public partial class Zamowienia : UserControl
    {
        public Zamowienia()
        {
            InitializeComponent();

            ICollectionView view = CollectionViewSource.GetDefaultView(OrdersListBox.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("DataWystawienia", ListSortDirection.Descending));
            view.GroupDescriptions.Add(new PropertyGroupDescription("DataWystawienia", new DocumentsByMonthsGrouper()));

            OrdersListBox.SelectedIndex = 0;
            SearchBoxName.Focus();
        }

        private ListaZamowien zamowienia = ListaZamowien.Instance;

        public ListaZamowien ListaDokumentow
        {
            get { return zamowienia; }
        }

        private void Remove(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz usunąć ten element?", "Czy na pewno?",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                zamowienia.Remove(OrdersListBox.SelectedItem as Zamowienie);
                OrdersListBox.Items.Refresh();
            }
        }

        private void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = OrdersListBox.SelectedItem != null;
        }

        private void GenerujFakture(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz stworzyć fakturę na podstawie zamówienia?", "Czy na pewno?",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Zamowienie z = OrdersListBox.SelectedItem as Zamowienie;
                Faktura fv = z.GenerujFakture();
                fv.Wprowadz();

                ListaFaktur.Instance.Add(fv);
                zamowienia.Remove(z);

                OrdersListBox.Items.Refresh();
                (Window.GetWindow(this) as MainWindow).RefreshInvoices();
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            OrdersListBox.Items.Refresh();
            OrdersListBox.SelectedIndex = 0;
        }

        private void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                if (OrdersListBox.Items.Count > 1)
                {
                    OrdersListBox.SelectedIndex = 1;
                }
                OrdersListBox.Focus();
            }
        }

        private void ClearSearch(object sender, ExecutedRoutedEventArgs e)
        {
            SearchBoxName.Text = "";
            SearchBoxNumber.Text = "";
            SearchBoxName.Focus();
        }

        private void Print(object sender, ExecutedRoutedEventArgs e)
        {
            (OrdersListBox.SelectedItem as Zamowienie).Buduj(new BudowniczyDruk());
        }

        private void SaveAsPDF(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog myDialog = new OpenFileDialog();
            myDialog.Filter =
                "PDF (*.PDF)|*.PDF" +
                "|All files (*.*)|*.*";
            myDialog.CheckFileExists = false;
            myDialog.Multiselect = false;
            if (myDialog.ShowDialog() == true)
            {
                (OrdersListBox.SelectedItem as Zamowienie).Buduj(new BudowniczyPDF(myDialog.FileName));
            }
        }

        private void GroupByComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OrdersListBox == null)
            {
                return;
            }

            ComboBoxItem selected = GroupByComboBox.SelectedItem as ComboBoxItem;

            ICollectionView view = CollectionViewSource.GetDefaultView(OrdersListBox.ItemsSource);
            view.GroupDescriptions.Clear();

            if ("Months".Equals(selected.Tag as string))
            {
                view.GroupDescriptions.Add(new PropertyGroupDescription("DataWystawienia", new DocumentsByMonthsGrouper()));
            }
            else if ("Customers".Equals(selected.Tag as string))
            {
                view.GroupDescriptions.Add(new PropertyGroupDescription("Klient.Nazwa"));
            }
        }

    }
}
