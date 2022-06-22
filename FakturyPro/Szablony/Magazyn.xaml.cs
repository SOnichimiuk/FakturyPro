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
using System.Windows.Media.Animation;
using FakturyPro.Klasy;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.ComponentModel;
using FakturyPro.Data.Models;
using FakturyPro.Services;
using FakturyPro.Data.Dto;
using FakturyPro.Interfaces;

namespace FakturyPro.Szablony
{
    /// <summary>
    /// Interaction logic for Magazyn.xaml
    /// </summary>
    public partial class Magazyn : UserControl
    {
        private IProductsService productsService;
        private IDocumentsService documentsService;
        private List<ProductDto> magazynTowarow;

        public Magazyn()
        {
            productsService = new ProductsService();
            documentsService = new DocumentsService();

            magazynTowarow = productsService.GetProducts();
            InitializeComponent();
            SearchBoxName.Focus();
        }


        public List<ProductDto> MagazynTowarow => magazynTowarow;


        private ObservableCollection<ProductDto> wybraneElementy = new ObservableCollection<ProductDto>();

        public ObservableCollection<ProductDto> WybraneElementy
        {
            get { return wybraneElementy; }
        }

        private void AddToList(object sender, ExecutedRoutedEventArgs e)
        {
            Storyboard sb = (Storyboard) FindResource("ArrowStoryboard");
            sb.Begin();
            foreach (ProductDto magazineProduct in StorageListBox.SelectedItems)
            {
                int vat = (int) magazineProduct.VatRate;
                int index = WybraneElementy.IndexOf(magazineProduct);
                if (index == -1)
                {
                    magazineProduct.Quantity = 1;
                    WybraneElementy.Add(magazineProduct);
                }
                else
                {
                    WybraneElementy[index].Quantity++;
                }
            }
            //StorageListBox.SelectedItems.Clear();
        }

        private void RemoveFromList(object sender, ExecutedRoutedEventArgs e)
        {
            Storyboard sb = (Storyboard) FindResource("ArrowReverseStoryboard");
            sb.Begin();

            List<ProductDto> lista = new List<ProductDto>();
            foreach (ProductDto product in SelectedProductsListBox.SelectedItems)
            {
                lista.Add(product);
            }

            foreach (ProductDto product in lista)
            {
                wybraneElementy.Remove(product);
            }

            //SelectedProductsListBox.SelectedItems.Clear();
        }

        private void Remove(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz usunąć ten towar?",
                    "Czy na pewno?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                List<ProductDto> lista = new List<ProductDto>();
                foreach (ProductDto product in StorageListBox.SelectedItems)
                {
                    lista.Add(product);
                }

                foreach (ProductDto product in lista)
                {
                    magazynTowarow.Remove(product);
                    productsService.DeleteProduct(product.Id);
                }

                StorageListBox.Items.Refresh();
            }
        }

        private void Edit(object sender, ExecutedRoutedEventArgs e)
        {
            ProductWindow win = new ProductWindow(StorageListBox.SelectedItems[0] as ProductDto);
            if (win.ShowDialog() == true)
            {
                productsService.UpdateProduct(win.Towar);
                StorageListBox.Items.Refresh();
            }
        }

        private void Add(object sender, ExecutedRoutedEventArgs e)
        {
            ProductWindow win = new ProductWindow(null);
            if (win.ShowDialog() == true)
            {
                productsService.AddProduct(win.Towar);
                magazynTowarow.Add(win.Towar);
                StorageListBox.Items.Refresh();
            }
        }

        private void StworzZamowienie(object sender, ExecutedRoutedEventArgs e)
        {
            DocumentDto zam = new DocumentDto();
            zam.Type = DocumentType.Order;
            foreach (ProductDto td in wybraneElementy)
            {
                zam.Products.Add(td);
            }

            AddDocumentWindow win = new AddDocumentWindow(zam);

            if (win.ShowDialog() == true)
            {
                documentsService.AddDocument(zam);

                wybraneElementy.Clear();
                MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
                if (mainWindow != null)
                {
                    mainWindow.RefreshOrders();
                }
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(StorageListBox.ItemsSource);
            if (string.IsNullOrWhiteSpace(SearchBoxName.Text))
            {
                view.Filter = delegate(object item) { return true; };
            }
            else
            {
                view.Filter = delegate(object item)
                {
                    ProductDto tm = item as ProductDto;
                    return (tm.Name.ToLower().Contains(SearchBoxName.Text.ToLower()));
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
                //MessageBox.Show(e.Error.ErrorContent.ToString());
            }
        }


        public bool IsValid(DependencyObject parent)
        {
            if (Validation.GetHasError(parent))
                return false;

            // Validate all the bindings on the children
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); ++i)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (!IsValid(child))
                {
                    return false;
                }
            }

            return true;
        }
    }
}