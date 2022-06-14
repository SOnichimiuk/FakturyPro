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
using Microsoft.Win32;
using FakturyPro.Konwertery;

namespace FakturyPro.Szablony
{
    /// <summary>
    /// Interaction logic for Faktury.xaml
    /// </summary>
    public partial class Faktury : UserControl
    {
        public Faktury()
        {
            InitializeComponent();

            ICollectionView view = CollectionViewSource.GetDefaultView(InvoicesListBox.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("DataWystawienia", ListSortDirection.Descending));
            view.GroupDescriptions.Add(new PropertyGroupDescription("DataWystawienia", new DocumentsByMonthsGrouper()));
            
            InvoicesListBox.SelectedIndex = 0;
            SearchBoxName.Focus();
        }

        private ListaFaktur faktury = ListaFaktur.Instance;

        public ListaFaktur ListaDokumentow
        {
            get { return faktury; }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            InvoicesListBox.Items.Refresh();
            InvoicesListBox.SelectedIndex = 0;
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
                Faktura fv = InvoicesListBox.SelectedItem as Faktura;
                fv.Usun();
                faktury.Remove(fv);
                InvoicesListBox.Items.Refresh();
            }
        }

        private void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            Faktura fv = InvoicesListBox.SelectedItem as Faktura;
            e.CanExecute = fv != null && !(fv.Stan is StanZaksiegowany);
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
                (InvoicesListBox.SelectedItem as Faktura).Zaksieguj();
                InvoicesListBox.Items.Refresh();
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
            (InvoicesListBox.SelectedItem as Faktura).Buduj(new BudowniczyDruk());
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
                (InvoicesListBox.SelectedItem as Faktura).Buduj(new BudowniczyPDF(myDialog.FileName));
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
                view.GroupDescriptions.Add(new PropertyGroupDescription("DataWystawienia", new DocumentsByMonthsGrouper()));
            }
            else if ("Customers".Equals(selected.Tag as string))
            {
                view.GroupDescriptions.Add(new PropertyGroupDescription("Klient.Nazwa"));
            }
        }
        
    }
}
