﻿using System;
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
using System.Windows.Media.Animation;
using FakturyPro.Klasy;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.ComponentModel;

namespace FakturyPro.Szablony
{
    /// <summary>
    /// Interaction logic for Magazyn.xaml
    /// </summary>
    public partial class Magazyn : UserControl
    {
        private bool hasErrors = false;
        
        public Magazyn()
        {
            InitializeComponent();
            SearchBoxName.Focus();
        }

        private FakturyPro.Klasy.Magazyn magazynTowarow = new LoggerMagazyn(FakturyPro.Klasy.PrawdziwyMagazyn.Instance);

        public FakturyPro.Klasy.Magazyn MagazynTowarow
        {
            get { return magazynTowarow; }
        }

        private ObservableCollection<TowarDokument> wybraneElementy = new ObservableCollection<TowarDokument>();

        public ObservableCollection<TowarDokument> WybraneElementy
        {
            get { return wybraneElementy; }
        }
        
        private void AddToList(object sender, ExecutedRoutedEventArgs e)
        {
            Storyboard sb = (Storyboard)FindResource("ArrowStoryboard");
            sb.Begin();
            TowarDokument td;
            foreach (TowarMagazyn tm in StorageListBox.SelectedItems)
            {
                td = new TowarDokument(tm) { Ilosc=1 };
                int index = WybraneElementy.IndexOf(td);
                if (index == -1)
                {
                    WybraneElementy.Add(td);
                }
                else
                {
                    WybraneElementy[index].Ilosc++;
                }
            }
            //StorageListBox.SelectedItems.Clear();
        }

        private void RemoveFromList(object sender, ExecutedRoutedEventArgs e)
        {
            Storyboard sb = (Storyboard)FindResource("ArrowReverseStoryboard");
            sb.Begin();

            List<TowarDokument> lista = new List<TowarDokument>();
            foreach (TowarDokument td in SelectedProductsListBox.SelectedItems)
            {
                lista.Add(td);
            }
            foreach (TowarDokument td in lista)
            {
                wybraneElementy.Remove(td);
            }

            //SelectedProductsListBox.SelectedItems.Clear();
        }

        private void Remove(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz usunąć ten towar?",
                "Czy na pewno?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                List<TowarMagazyn> lista = new List<TowarMagazyn>();
                foreach (TowarMagazyn tm in StorageListBox.SelectedItems)
                {
                    lista.Add(tm);
                }
                foreach (TowarMagazyn tm in lista)
                {
                    magazynTowarow.Remove(tm);
                    wybraneElementy.Remove(new TowarDokument(tm));
                }
                StorageListBox.Items.Refresh();
            }
        }

        private void Edit(object sender, ExecutedRoutedEventArgs e)
        {
            ProductWindow win = new ProductWindow(StorageListBox.SelectedItems[0] as TowarMagazyn);
            if (win.ShowDialog() == true)
            {
                StorageListBox.Items.Refresh();
            }
                
        }

        private void Add(object sender, ExecutedRoutedEventArgs e)
        {
            ProductWindow win = new ProductWindow(null);
            if (win.ShowDialog() == true)
            {
                magazynTowarow.Add(win.Towar);
                StorageListBox.Items.Refresh();
            }

        }

        private void StworzFakture(object sender, ExecutedRoutedEventArgs e)
        {
            Faktura fv = new Faktura();
            foreach (TowarDokument td in wybraneElementy)
            {
                fv.Add(td);
            }
            
            AddDocumentWindow win = new AddDocumentWindow(fv);

            if (win.ShowDialog() == true)
            {
                fv = win.Dokument as Faktura;
                fv.Wprowadz();
                ListaFaktur.Instance.Add(fv);
                wybraneElementy.Clear();
                MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
                if (mainWindow != null)
                {
                    mainWindow.RefreshInvoices();
                }

                switch (win.WybranaAkcja)
                {
                    case AddDocumentWindow.ChosenAction.SavePDF:
                        // Zapis do PDF
                        OpenFileDialog myDialog = new OpenFileDialog();
                        myDialog.Filter =
                            "PDF (*.PDF)|*.PDF" +
                            "|All files (*.*)|*.*";
                        myDialog.CheckFileExists = false;
                        myDialog.Multiselect = false;
                        if (myDialog.ShowDialog() == true)
                        {
                            fv.Buduj(new BudowniczyPDF(myDialog.FileName));
                        }
                        

                        break;
                    case AddDocumentWindow.ChosenAction.SavePrint:
                        // Drukowanie
                        fv.Buduj(new BudowniczyDruk());
                        break;
                }
            }
        }

        private void StworzZamowienie(object sender, ExecutedRoutedEventArgs e)
        {
            Zamowienie zm = new Zamowienie();
            foreach (TowarDokument td in wybraneElementy)
            {
                zm.Add(td);
            }

            AddDocumentWindow win = new AddDocumentWindow(zm);

            if (win.ShowDialog() == true)
            {
                ListaZamowien.Instance.Add((Zamowienie)win.Dokument);
                wybraneElementy.Clear();
                MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
                if (mainWindow != null)
                {
                    mainWindow.RefreshOrders();
                }

                switch (win.WybranaAkcja)
                {
                    case AddDocumentWindow.ChosenAction.SavePDF:
                        // Zapis do PDF
                        OpenFileDialog myDialog = new OpenFileDialog();
                        myDialog.Filter =
                            "PDF (*.PDF)|*.PDF" +
                            "|All files (*.*)|*.*";
                        myDialog.CheckFileExists = false;
                        myDialog.Multiselect = false;
                        if (myDialog.ShowDialog() == true)
                        {
                            zm.Buduj(new BudowniczyPDF(myDialog.FileName));
                        }


                        break;
                    case AddDocumentWindow.ChosenAction.SavePrint:
                        // Drukowanie
                        zm.Buduj(new BudowniczyDruk());
                        break;
                }
            }
        }

        private void Przyjmij(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz przyjąć produkty na stan?",
                "Czy na pewno?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach (TowarDokument td in wybraneElementy)
                {
                    td.WprowadzNaStan();
                }
                wybraneElementy.Clear();
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(StorageListBox.ItemsSource);
            if (string.IsNullOrWhiteSpace(SearchBoxName.Text))
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
                    TowarMagazyn tm = item as TowarMagazyn;
                    return (tm.Nazwa.ToLower().Contains(SearchBoxName.Text.ToLower()));
                };
            }
            StorageListBox.SelectedIndex = 0;
        }

        private void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                if (StorageListBox.Items.Count > 1)
                {
                    StorageListBox.SelectedIndex = 1;
                }
                StorageListBox.Focus();
            }
        }

        private void ClearSearch(object sender, ExecutedRoutedEventArgs e)
        {
            SearchBoxName.Text = "";
            SearchBoxName.Focus();
        }

        private void CanCreateDocument(object sender, CanExecuteRoutedEventArgs e)
        {
            if (wybraneElementy.Count < 1)
            {
                e.CanExecute = false;
                return;
            }

            e.CanExecute = IsValid(SelectedProductsListBox);
        }

        private void AddToListCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = StorageListBox.SelectedItems.Count > 0;
        }

        private void RemoveFromListCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = SelectedProductsListBox.SelectedItems.Count > 0;
        }

        private void RemoveCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = StorageListBox.SelectedItems.Count > 0;
        }

        private void SelectedProductsListBox_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                hasErrors = true;
                //MessageBox.Show(e.Error.ErrorContent.ToString());
            }
        }


       /* private bool IsValid(DependencyObject obj)
        {
            // The dependency object is valid if it has no errors, 
            // and all of its children (that are dependency objects) are error-free.

            //System.Collections.IEnumerable siema = LogicalTreeHelper.GetChildren(obj)
            //    .OfType<DependencyObject>();
            //TextBox tb = obj as TextBox;
            //if (tb != null)
            //{
            //    Console.WriteLine(tb.Text);
            //}

            return ! Validation.GetHasError(obj) &&
                LogicalTreeHelper.GetChildren(obj)
                .OfType<TextBox>()
                .All(child => IsValid(child));
        }*/

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
