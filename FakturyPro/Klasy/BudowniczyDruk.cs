using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Markup;

namespace FakturyPro.Klasy
{
    class BudowniczyDruk : Budowniczy
    {
        PrintDialog pd = new PrintDialog();
        FixedDocument doc = new FixedDocument();
        FixedPage page = new FixedPage();
        Grid strona = new Grid();
        Grid gridTowary = new Grid();
        public BudowniczyDruk()
        {
            doc.DocumentPaginator.PageSize = new Size(pd.PrintableAreaWidth, pd.PrintableAreaHeight);
            strona.ClipToBounds = false;
            strona.ShowGridLines = false;

            ColumnDefinition colDef1 = new ColumnDefinition();
            ColumnDefinition colDef2 = new ColumnDefinition();
            ColumnDefinition colDef3 = new ColumnDefinition();
            ColumnDefinition colDef4 = new ColumnDefinition();
            ColumnDefinition colDef5 = new ColumnDefinition();
            ColumnDefinition colDef6 = new ColumnDefinition();
            ColumnDefinition colDef7 = new ColumnDefinition();
            gridTowary.ColumnDefinitions.Add(colDef1);
            gridTowary.ColumnDefinitions.Add(colDef2);
            gridTowary.ColumnDefinitions.Add(colDef3);
            gridTowary.ColumnDefinitions.Add(colDef4);
            gridTowary.ColumnDefinitions.Add(colDef5);
            gridTowary.ColumnDefinitions.Add(colDef6);
            gridTowary.ColumnDefinitions.Add(colDef7);

            RowDefinition rowDef1 = new RowDefinition();
            gridTowary.RowDefinitions.Add(rowDef1);

            TextBlock tb1 = new TextBlock();
            TextBlock tb2 = new TextBlock();
            TextBlock tb3 = new TextBlock();
            TextBlock tb4 = new TextBlock();
            TextBlock tb5 = new TextBlock();
            TextBlock tb6 = new TextBlock();
            TextBlock tb7 = new TextBlock();
            tb1.Text = "Lp.";
            tb1.Margin = new Thickness(5);
            tb2.Text = "Nazwa";
            tb2.Margin = new Thickness(5);
            tb3.Text = "Jednostka";
            tb3.Margin = new Thickness(5);
            tb4.Text = "Ilość";
            tb4.Margin = new Thickness(5);
            tb5.Text = "Cena netto";
            tb5.Margin = new Thickness(5);
            tb6.Text = "Vat(%)";
            tb6.Margin = new Thickness(5);
            tb7.Text = "WArtość brutto";
            tb7.Margin = new Thickness(5);

            Grid.SetRow(tb1, 0);
            Grid.SetColumn(tb1, 0);
            Grid.SetRow(tb2, 0);
            Grid.SetColumn(tb2, 1);
            Grid.SetRow(tb3, 0);
            Grid.SetColumn(tb3, 2);
            Grid.SetRow(tb4, 0);
            Grid.SetColumn(tb4, 3);
            Grid.SetRow(tb5, 0);
            Grid.SetColumn(tb5, 4);
            Grid.SetRow(tb6, 0);
            Grid.SetColumn(tb6, 5);
            Grid.SetRow(tb7, 0);
            Grid.SetColumn(tb7, 6);

            gridTowary.Children.Add(tb1);
            gridTowary.Children.Add(tb2);
            gridTowary.Children.Add(tb3);
            gridTowary.Children.Add(tb4);
            gridTowary.Children.Add(tb5);
            gridTowary.Children.Add(tb6);
            gridTowary.Children.Add(tb7);

            page.Width = doc.DocumentPaginator.PageSize.Width;
            page.Height = doc.DocumentPaginator.PageSize.Height;

            ColumnDefinition colDef11 = new ColumnDefinition();
            ColumnDefinition colDef22 = new ColumnDefinition();
            strona.ColumnDefinitions.Add(colDef11);
            strona.ColumnDefinitions.Add(colDef22);
        }
        public void BudujNaglowek(string naglowek)
        {
            TextBlock textNaglowek = new TextBlock();
            textNaglowek.Text = naglowek;
            textNaglowek.FontSize = 28;
            textNaglowek.TextAlignment = TextAlignment.Center;
            textNaglowek.Margin = new Thickness(96);
            RowDefinition rowDef11 = new RowDefinition();
            strona.RowDefinitions.Add(rowDef11);
            Grid.SetColumnSpan(textNaglowek, 2);
            Grid.SetRow(textNaglowek, 0);
            strona.Children.Add(textNaglowek);
            strona.Margin = new Thickness(96);
        }

        public void BudujAdresy(Kontrahent klient, Kontrahent sprzedawca)
        {
            String klientS = klient.Nazwa + "\n" + klient.Adres + "\n" + klient.Miasto + " " + klient.KodPocztowy +
                 "\nNIP: " + klient.NIP;
            String sprzedawcaS = sprzedawca.Nazwa + "\n" + sprzedawca.Adres + "\n" + sprzedawca.Miasto + " " + sprzedawca.KodPocztowy +
                 "\nNIP: " + sprzedawca.NIP;
            //Grid gridAdresy = new Grid();
            ////gridAdresy.HorizontalAlignment = HorizontalAlignment.Left;
            ////gridAdresy.VerticalAlignment = VerticalAlignment.Top;
            //gridAdresy.ShowGridLines = true;

            //ColumnDefinition colDef1 = new ColumnDefinition();
            //ColumnDefinition colDef2 = new ColumnDefinition();
            //gridAdresy.ColumnDefinitions.Add(colDef1);
            //gridAdresy.ColumnDefinitions.Add(colDef2);

            //RowDefinition rowDef1 = new RowDefinition();
            //gridAdresy.RowDefinitions.Add(rowDef1);

            TextBlock sprzed = new TextBlock();
            sprzed.Text = sprzedawcaS;
            sprzed.FontSize = 12;
            sprzed.FontWeight = FontWeights.Bold;
            Grid.SetRow(sprzed, 1);
            Grid.SetColumn(sprzed, 0);

            TextBlock kli = new TextBlock();
            kli.Text = klientS;
            kli.FontSize = 12;
            kli.FontWeight = FontWeights.Bold;
            Grid.SetRow(kli, 1);
            Grid.SetColumn(kli, 1);

            //strona.Children.Add(sprzed);
            //strona.Children.Add(kli);
            //strona.Children.Add(strona);

            RowDefinition rowDef11 = new RowDefinition();
            strona.RowDefinitions.Add(rowDef11);
            sprzed.Margin = new Thickness(15);
            strona.Children.Add(sprzed);
            kli.Margin = new Thickness(15);
            strona.Children.Add(kli);
        }
        public void BudujTowar(int lp, TowarDokument t)
        {
            RowDefinition rowDef1 = new RowDefinition();
            gridTowary.RowDefinitions.Add(rowDef1);

            TextBlock tb1 = new TextBlock();
            TextBlock tb2 = new TextBlock();
            TextBlock tb3 = new TextBlock();
            TextBlock tb4 = new TextBlock();
            TextBlock tb5 = new TextBlock();
            TextBlock tb6 = new TextBlock();
            TextBlock tb7 = new TextBlock();

            tb1.Text = lp.ToString();
            tb1.Margin = new Thickness(5);
            tb2.Text = t.Nazwa;
            tb2.Margin = new Thickness(5);
            tb3.Text = t.Jednostka.ToString();
            tb3.Margin = new Thickness(5);
            tb4.Text = t.Ilosc.ToString();
            tb4.Margin = new Thickness(5);
            tb5.Text = t.CenaNetto.ToString();
            tb5.Margin = new Thickness(5);
            tb6.Text = t.Vat.ToString();
            tb6.Margin = new Thickness(5);
            tb7.Text = t.WartoscBrutto.ToString();
            tb7.Margin = new Thickness(5);

            Grid.SetRow(tb1, lp);
            Grid.SetColumn(tb1, 0);
            Grid.SetRow(tb2, lp);
            Grid.SetColumn(tb2, 1);
            Grid.SetRow(tb3, lp);
            Grid.SetColumn(tb3, 2);
            Grid.SetRow(tb4, lp);
            Grid.SetColumn(tb4, 3);
            Grid.SetRow(tb5, lp);
            Grid.SetColumn(tb5, 4);
            Grid.SetRow(tb6, lp);
            Grid.SetColumn(tb6, 5);
            Grid.SetRow(tb7, lp);
            Grid.SetColumn(tb7, 6);

            gridTowary.Children.Add(tb1);
            gridTowary.Children.Add(tb2);
            gridTowary.Children.Add(tb3);
            gridTowary.Children.Add(tb4);
            gridTowary.Children.Add(tb5);
            gridTowary.Children.Add(tb6);
            gridTowary.Children.Add(tb7);
        }

        public void BudujPodliczenie(double razemBrutto)
        {
            RowDefinition rowDef1 = new RowDefinition();
            gridTowary.RowDefinitions.Add(rowDef1);
            int rows = gridTowary.RowDefinitions.Count();
            TextBlock tb1 = new TextBlock();
            TextBlock tb2 = new TextBlock();

            tb1.Text = "razem";
            tb1.Margin = new Thickness(5);
            tb2.Text = razemBrutto.ToString();
            tb2.Margin = new Thickness(5);

            Grid.SetRow(tb1, rows-1);
            Grid.SetColumn(tb1, 5);
            Grid.SetRow(tb2, rows-1);
            Grid.SetColumn(tb2, 6);

            gridTowary.Children.Add(tb1);
            gridTowary.Children.Add(tb2);
            //strona.Children.Add(gridTowary);
            
            
        }

        public void ZakonczTowary()
        {
            RowDefinition rowDef11 = new RowDefinition();
            strona.RowDefinitions.Add(rowDef11);
            Grid.SetColumnSpan(gridTowary, 2);
            Grid.SetRow(gridTowary, 2);
            strona.Children.Add(gridTowary);
        }

        public void BudujPodpisy()
        {
            TextBlock podpis1 = new TextBlock();
            TextBlock podpis2 = new TextBlock();
            podpis1.Text = "Podpis upoważnionego\ndo wystawienia faktury";
            podpis2.Text = "Podpis odbierającego";
            podpis1.FontSize = 10;
            podpis2.FontSize = 10;
            //podpis1.TextAlignment = TextAlignment.Left;
            //podpis2.TextAlignment = TextAlignment.Right;
            //podpis1.Margin = new Thickness(96);
            //podpis2.Margin = new Thickness(96);
            //strona.Children.Add(podpis1);
            //strona.Children.Add(podpis2);

            RowDefinition rowDef11 = new RowDefinition();
            strona.RowDefinitions.Add(rowDef11);
            Grid.SetRow(podpis1, 3);
            Grid.SetColumn(podpis1, 0);
            Grid.SetRow(podpis2, 3);
            Grid.SetColumn(podpis2, 1);
            podpis1.Margin = new Thickness(70);
            podpis2.Margin = new Thickness(70);
            strona.Children.Add(podpis1);
            strona.Children.Add(podpis2);
            page.Children.Add(strona);
        }        

        public void DajWynik()
        {
            PageContent page1Content = new PageContent();
            ((IAddChild)page1Content).AddChild(page);
            doc.Pages.Add(page1Content);

            
            if(pd.ShowDialog() != true)return;

            try
            {
                pd.PrintDocument(doc.DocumentPaginator, "Faktura");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
