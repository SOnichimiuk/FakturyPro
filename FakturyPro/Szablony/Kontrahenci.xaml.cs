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
using FakturyPro.Services;
using FakturyPro.Data.Dto;
using FakturyPro.Interfaces;

namespace FakturyPro.Szablony
{
    /// <summary>
    /// Interaction logic for Kontrahenci.xaml
    /// </summary>
    public partial class Kontrahenci : UserControl
    {
        private IClientsService clientsService;
        public List<ClientDto> klienci;
        public Kontrahenci()
        {
            clientsService = new ClientsService();
            klienci = clientsService.GetClients();
            InitializeComponent();
            CustomersListBox.SelectedIndex = 0;
        }

        private void Remove(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz usunąć ten element?", "Czy na pewno?",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var klient = CustomersListBox.SelectedItem as ClientDto;
                clientsService.DeleteClient(klient.Id);
                klienci.Remove(CustomersListBox.SelectedItem as ClientDto);
                CustomersListBox.Items.Refresh();
            }
        }

        private void Edit(object sender, ExecutedRoutedEventArgs e)
        {
            CustomerWindow win = new CustomerWindow(CustomersListBox.SelectedItem as ClientDto);
            if (win.ShowDialog() == true)
            {
                clientsService.UpdateClient(win.Klient);
                CustomersListBox.Items.Refresh();
            }
            //klienci.Remove(CustomersListBox.SelectedItem as Kontrahent);
        }

        private void Add(object sender, ExecutedRoutedEventArgs e)
        {
            CustomerWindow win = new CustomerWindow(null);
            if (win.ShowDialog() == true)
            {
                clientsService.AddClient(win.Klient);
                klienci.Add(win.Klient);
                CustomersListBox.Items.Refresh();
            }
            //klienci.Remove(CustomersListBox.SelectedItem as Kontrahent);
        }

        private void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = CustomersListBox.SelectedItem != null;
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
