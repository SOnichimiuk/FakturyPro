using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Runtime.CompilerServices;
using FakturyPro.Data.Dto;
using FakturyPro.Interfaces;
using FakturyPro.Services;

namespace FakturyPro.Szablony
{
    /// <summary>
    /// Interaction logic for Zamowienia.xaml
    /// </summary>
    public partial class Zamowienia : UserControl, INotifyPropertyChanged
    {
        private IDocumentsService documentsService;
        private ObservableCollection<DocumentDto> listaZamowien;

        public ObservableCollection<DocumentDto> ListaZamowien
        {
            get
            {
                return listaZamowien;
            }
            set
            {
                listaZamowien = value;
                OnPropertyChanged("ListaZamowien");
            }
        }

        public Zamowienia()
        {
            documentsService = new DocumentsService();
            DownloadItems();
            
            InitializeComponent();

            ICollectionView view = CollectionViewSource.GetDefaultView(OrdersListBox.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("CreationDate", ListSortDirection.Descending));
            view.GroupDescriptions.Add(new PropertyGroupDescription("CreationDate", new DocumentsByMonthsGrouper()));

            OrdersListBox.SelectedIndex = 0;
            SearchBoxName.Focus();
        }

        public void DownloadItems()
        {
            ListaZamowien = new ObservableCollection<DocumentDto>(documentsService.GetOrders());
        }
        
        public string ClientName { get; set; }
        public string DocumentNr { get; set; }
        
        private bool Filter(object item)
        {
            if (string.IsNullOrEmpty(ClientName) && string.IsNullOrEmpty(DocumentNr))
            {
                return true;
            }

            DocumentDto document = item as DocumentDto;
            
            return (string.IsNullOrEmpty(ClientName) || document.ClientName.ToLower().Contains(ClientName.ToLower()))
                   && (string.IsNullOrEmpty(DocumentNr) || document.DocumentNr.ToLower().Contains(DocumentNr.ToLower()));
        }
        
        private void Remove(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz usunąć ten element?", "Czy na pewno?",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                DocumentDto document = OrdersListBox.SelectedItem as DocumentDto;
                listaZamowien.Remove(document);
                documentsService.DeleteDocument(document.Id);
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
                DocumentDto doc = OrdersListBox.SelectedItem as DocumentDto;
                listaZamowien.Remove(doc);

                doc.Type = DocumentType.Invoice;
                doc.DocumentNr = $"ZW/{doc.Id}";
                
                documentsService.UpdateDocument(doc);
                
                (Window.GetWindow(this) as MainWindow).RefreshInvoices();
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionView view = CollectionViewSource.GetDefaultView(OrdersListBox.ItemsSource) as CollectionView;
            view.Filter = Filter;
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
            //(OrdersListBox.SelectedItem as DocumentDto).Buduj(new BudowniczyDruk());
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
                //(OrdersListBox.SelectedItem as DocumentDto).Buduj(new BudowniczyPDF(myDialog.FileName));
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
                view.GroupDescriptions.Add(new PropertyGroupDescription("CreationDate", new DocumentsByMonthsGrouper()));
            }
            else if ("Customers".Equals(selected.Tag as string))
            {
                view.GroupDescriptions.Add(new PropertyGroupDescription("ClientName"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
