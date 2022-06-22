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
using System.ComponentModel;
using System.Collections;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using FakturyPro.Data.Dto;
using FakturyPro.Data.Models;
using FakturyPro.Interfaces;
using Microsoft.Win32;
using FakturyPro.Konwertery;
using FakturyPro.Services;

namespace FakturyPro.Szablony
{
    /// <summary>
    /// Interaction logic for Faktury.xaml
    /// </summary>
    public partial class Faktury : UserControl, INotifyPropertyChanged
    {
        private IDocumentsService documentsService;
        private ObservableCollection<DocumentDto> listaFaktur;

        public ObservableCollection<DocumentDto> ListaFaktur
        {
            get
            {
                return listaFaktur;
            }
            set
            {
                listaFaktur = value;
                OnPropertyChanged("ListaFaktur");
            }
        }
        
        public Faktury()
        {
            documentsService = new DocumentsService();
            DownloadItems();
            
            InitializeComponent();

            ICollectionView view = CollectionViewSource.GetDefaultView(InvoicesListBox.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("CreationDate", ListSortDirection.Descending));
            view.GroupDescriptions.Add(new PropertyGroupDescription("CreationDate", new DocumentsByMonthsGrouper()));
            
            InvoicesListBox.SelectedIndex = 0;
            SearchBoxName.Focus();
        }

        public void DownloadItems()
        {
            ListaFaktur = new ObservableCollection<DocumentDto>(documentsService.GetInvoices());
        }

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
        
        public string ClientName { get; set; }
        public string DocumentNr { get; set; }
        
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionView view = CollectionViewSource.GetDefaultView(InvoicesListBox.ItemsSource) as CollectionView;
            view.Filter = Filter;
        }

        private void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                if (InvoicesListBox.Items.Count > 1)
                {
                    InvoicesListBox.SelectedIndex = 1;
                }
                InvoicesListBox.Focus();
            }
        }

        private void Remove(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz usunąć ten element?", "Czy na pewno?",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                DocumentDto doc = InvoicesListBox.SelectedItem as DocumentDto;
                
                documentsService.DeleteDocument(doc.Id);
                ListaFaktur.Remove(doc);
            }
        }

        private void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            DocumentDto doc = InvoicesListBox.SelectedItem as DocumentDto;
            e.CanExecute = doc != null && doc.State != DocumentState.Booked;
        }

        private void IsSelected(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = InvoicesListBox.SelectedItem != null;
        }

        private void Ksieguj(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz zaksięgować element?", "Czy na pewno?",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                DocumentDto doc = InvoicesListBox.SelectedItem as DocumentDto;
                doc.State = DocumentState.Booked;

                documentsService.UpdateDocument(doc);
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
            //(InvoicesListBox.SelectedItem as DocumentDto).Buduj(new BudowniczyDruk());
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
                //(InvoicesListBox.SelectedItem as DocumentDto).Buduj(new BudowniczyPDF(myDialog.FileName));
            }
        }

        private void GroupByComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InvoicesListBox == null)
            {
                return;
            }

            ComboBoxItem selected = GroupByComboBox.SelectedItem as ComboBoxItem;

            ICollectionView view = CollectionViewSource.GetDefaultView(InvoicesListBox.ItemsSource);
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
