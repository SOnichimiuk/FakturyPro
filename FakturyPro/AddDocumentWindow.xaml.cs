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
using System.ComponentModel;
using FakturyPro.Interfaces;
using FakturyPro.Data.Dto;
using FakturyPro.Services;

namespace FakturyPro
{
    /// <summary>
    /// Interaction logic for AddDocument.xaml
    /// </summary>
    public partial class AddDocumentWindow : Window
    {
        private IClientsService clientsService;
        private List<ClientDto> klienci;
        public AddDocumentWindow()
        {
            clientsService = new ClientsService();
            klienci = clientsService.GetClients();
            //dokument = doc;
            InitializeComponent();
            CustomersListBox.SelectedIndex = 0;
            SearchBoxName.Focus();
        }

        public List<ClientDto> Klienci => klienci;

        private ChosenAction akcja;

        public ChosenAction WybranaAkcja
        {
            get { return akcja; }
        }


        private DocumentDto dokument = new DocumentDto();

        public DocumentDto Dokument
        {
            get { return dokument; }
        }

        private void Save(object sender, ExecutedRoutedEventArgs e)
        {
            string param = (string) e.Parameter;

            akcja = ChosenAction.Save;

            if (param == "SavePDF")
            {
                akcja = ChosenAction.SavePDF;
            }
            else if (param == "SavePrint")
            {
                akcja = ChosenAction.SavePrint;
            }
            var client = CustomersListBox.SelectedItem as ClientDto;
            dokument.ClientId = client.Id;
            dokument.ClientName = client.Name;

            DialogResult = true;
            Close();
        }

        private void Cancel(object sender, ExecutedRoutedEventArgs e)
        {
            akcja = ChosenAction.Cancel;
            DialogResult = false;
            Close();
        }

        private void CanSave(object sender, CanExecuteRoutedEventArgs e)
        {
            if (CustomersListBox.SelectedItem == null)
            {
                e.CanExecute = false;
                return;
            }
            /*
            foreach (TowarDokument item in dokument)
            {
                if (!string.IsNullOrEmpty(item.Error))
                {
                    e.CanExecute = false;
                    return;
                }
            }*/
            e.CanExecute = true;
        }

        public enum ChosenAction
        {
            Save,
            SavePDF,
            SavePrint,
            Cancel
        }


        private void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                if (CustomersListBox.Items.Count > 1)
                {
                    CustomersListBox.SelectedIndex = 1;
                }
                CustomersListBox.Focus();
            }
        }

        private void SearchBox_Changed(object sender, TextChangedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(CustomersListBox.ItemsSource);
            if (string.IsNullOrWhiteSpace(SearchBoxName.Text) && string.IsNullOrWhiteSpace(SearchBoxNIP.Text))
            {
                view.Filter = delegate(object item)
                {
                    return true;
                };
            }
            else
            {
                view.Filter = delegate(object item)
                {
                    ClientDto klient = item as ClientDto;
                    return (klient.Name.ToLower().Contains(SearchBoxName.Text.ToLower()) && klient.NIP.Contains(SearchBoxNIP.Text));
                };
            }
            CustomersListBox.SelectedIndex = 0;
        }

        private void ClearSearch(object sender, ExecutedRoutedEventArgs e)
        {
            SearchBoxName.Text = "";
            SearchBoxNIP.Text = "";
            SearchBoxName.Focus();
        }
        
    }
}
