using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace FakturyPro.Klasy
{
    class BudowniczyPDF : Budowniczy
    {
        Document mojDok = new Document(PageSize.A4);//.Rotate());
        PdfWriter writer;
        PdfPTable adresy = new PdfPTable(2);
        PdfPTable towary = new PdfPTable(7);
        PdfPTable podsumowanie = new PdfPTable(2);
        PdfPTable xx = new PdfPTable(1);
        string filename;
        PdfPCell x;

        public BudowniczyPDF(string filename)
        {
            this.filename = filename;
           
             PdfPCell komorka = new PdfPCell(new Phrase("tutaj twój tekst", new Font(Font.FontFamily.TIMES_ROMAN, 18f, Font.BOLD, BaseColor.BLACK)));
            komorka.BackgroundColor = BaseColor.GRAY;
            komorka.Border = Rectangle.NO_BORDER;



            x = new PdfPCell(new Phrase("Suma meldunków za ten okres rozliczeniowy:", new Font(Font.FontFamily.TIMES_ROMAN, 18f, Font.BOLD, BaseColor.BLACK)));
            x.BackgroundColor = BaseColor.GRAY;            
            x.Border = Rectangle.NO_BORDER;

            xx.AddCell(x);

            float[] widthsA = new float[] { 1f, 1f };
            adresy.TotalWidth = mojDok.PageSize.Width - 36f;
            adresy.LockedWidth = true;
            adresy.SetWidths(widthsA);
            adresy.SpacingAfter = 50f;

            adresy.AddCell("Sprzedawca");
            adresy.AddCell("Nabywca");

            float[] widthsT = new float[] { 1f, 1f, 1f, 1f, 1f, 1f, 1f};
            towary.TotalWidth = mojDok.PageSize.Width - 200f;
            towary.LockedWidth = true;
            towary.SetWidths(widthsT);

            towary.AddCell("lp.");
            towary.AddCell("Nazwa");
            towary.AddCell("Jednostka");
            towary.AddCell("Ilość");
            towary.AddCell("Cena netto");
            towary.AddCell("VAT(%)");
            towary.AddCell("Wartość brutto");

            float[] widthsP = new float[] { 1f, 1f };
            podsumowanie.TotalWidth = 200f;
            podsumowanie.LockedWidth = true;
            podsumowanie.SetWidths(widthsP);
            podsumowanie.SpacingAfter = 100f;
            podsumowanie.SpacingBefore = 30f;
            podsumowanie.HorizontalAlignment = Element.ALIGN_RIGHT;

            podsumowanie.AddCell("Razem");
        }

        public void BudujNaglowek(String naglowek)
        {
            writer = PdfWriter.GetInstance(mojDok, new FileStream(filename, FileMode.Create));
            mojDok.Open();
            mojDok.Add(xx);
            Paragraph head = new Paragraph(naglowek, new Font(Font.FontFamily.TIMES_ROMAN, 28f, Font.BOLD));
            head.SpacingAfter = 18f;
            head.Alignment = Element.ALIGN_CENTER;
            mojDok.Add(head);
        }

        public void BudujAdresy(Kontrahent klient, Kontrahent sprzedawca)
        {
            String klientS = klient.Nazwa + "\n" + klient.Adres + "\n" + klient.Miasto + " " + klient.KodPocztowy +
                 "\nNIP: " + klient.NIP;
            String sprzedawcaS = sprzedawca.Nazwa + "\n" + sprzedawca.Adres + "\n" + sprzedawca.Miasto + " " + sprzedawca.KodPocztowy +
                 "\nNIP: " + sprzedawca.NIP;
            adresy.AddCell(new Phrase(klientS, new Font(Font.FontFamily.TIMES_ROMAN, 18f, Font.BOLD)));
            adresy.AddCell(new Phrase(sprzedawcaS, new Font(Font.FontFamily.TIMES_ROMAN, 18f, Font.BOLD)));
            mojDok.Add(adresy);
        }

        public void BudujTowar(int lp, TowarDokument t)
        {
            towary.AddCell(lp.ToString());
            towary.AddCell(t.Nazwa);
            towary.AddCell(t.Jednostka.ToString());
            towary.AddCell(t.Ilosc.ToString());
            towary.AddCell(t.CenaNetto.ToString());
            towary.AddCell(t.Vat.ToString());
            towary.AddCell(t.WartoscBrutto.ToString());
        }

        public void ZakonczTowary()
        {
            mojDok.Add(towary);
        }

        public void BudujPodliczenie(double razemBrutto)
        {
            podsumowanie.AddCell(razemBrutto.ToString());
            mojDok.Add(podsumowanie);
        }

        public void BudujPodpisy()
        {
            Paragraph podpisS = new Paragraph("Podpis upoważnionego\ndo wystawienia faktury", 
                new Font(Font.FontFamily.TIMES_ROMAN, 7f, Font.BOLD));
            Paragraph podpisK = new Paragraph("Podpis odbierającego", 
                new Font(Font.FontFamily.TIMES_ROMAN, 7f, Font.BOLD));

            podpisS.Alignment = Element.ALIGN_LEFT;
            podpisK.Alignment = Element.ALIGN_RIGHT;            
        }

        public void DajWynik()
        {
            mojDok.Close();
        }
    }
}