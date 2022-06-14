using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using FakturyPro.Klasy;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace FakturyPro
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void ApplicationStartup(object sender, StartupEventArgs e)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream("Sprzedawca.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
                App.Current.Properties["Sprzedawca"] = formatter.Deserialize(stream) as Kontrahent;
                stream.Close();
                stream = new FileStream("Magazyn.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
                PrawdziwyMagazyn.Instance.Deserializuj(stream, formatter);
                stream.Close();
                stream = new FileStream("Klienci.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
                ListaKlientow.Instance.Deserializuj(stream, formatter);
                stream.Close();
                stream = new FileStream("Faktury.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
                ListaFaktur.Instance.Deserializuj(stream, formatter);
                stream.Close();
                stream = new FileStream("Zamowienia.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
                ListaZamowien.Instance.Deserializuj(stream, formatter);
                stream.Close();
            }
            catch (FileNotFoundException ex)
            {
                App.Current.Properties["Sprzedawca"] = new Kontrahent
                {
                    Nazwa = "PatternWPF Sp. z o.o.",
                    Adres = "ul. Wstążkowa 9",
                    Miasto = "Białystok",
                    KodPocztowy = "15-555",
                    NIP = "966-234-22-44"
                };

                Magazyn magazyn = new LoggerMagazyn(PrawdziwyMagazyn.Instance);
                TowarMagazyn t1 = new TowarMagazyn() { Nazwa = "KFA2 GeForce RTX 3060 Ti 1-Click OC LHR 8GB GDDR6", Ilosc = 18, Jednostka = Towar.JednostkaIlosci.szt, CenaBrutto = 4918.77 };
                TowarMagazyn t2 = new TowarMagazyn() { Nazwa = "MSI Radeon RX 6600 XT MECH 2X OC 8GB GDDR6", Ilosc = 7, Jednostka = Towar.JednostkaIlosci.szt, CenaBrutto = 4426.77 };
                TowarMagazyn t3 = new TowarMagazyn() { Nazwa = "Roccat KHAN AIMO - 7.1 High Resolution RGB Gaming", Ilosc = 1, Jednostka = Towar.JednostkaIlosci.szt, CenaNetto = 519 };
                magazyn.Add(t1);
                magazyn.Add(t2);
                magazyn.Add(new TowarMagazyn() { Nazwa = "Samsung Galaxy Watch 4 Classic Stainless 46mm Silver LTE", Ilosc = 4, Jednostka = Towar.JednostkaIlosci.szt, CenaNetto = 1699.00 });
                magazyn.Add(new TowarMagazyn() { Nazwa = "Suszarka do włosów BaByliss", Ilosc = 11, Jednostka = Towar.JednostkaIlosci.szt, CenaNetto = 332.52 });
                magazyn.Add(new TowarMagazyn() { Nazwa = "Samsung 120GB 2,5 SATA SSD Seria 840 Basic", Ilosc = 21, Jednostka = Towar.JednostkaIlosci.szt, CenaNetto = 399 });
                magazyn.Add(t3);
                magazyn.Add(new TowarMagazyn() { Nazwa = "Klawiatura Logitech G PRO League of Legends Wave2", Ilosc = 2, Jednostka = Towar.JednostkaIlosci.szt, CenaNetto = 405.69 });

                ListaFaktur faktury = ListaFaktur.Instance;
                ListaZamowien zamowienia = ListaZamowien.Instance;
                ListaKlientow klienci = ListaKlientow.Instance;

                klienci.Add(new Kontrahent() { Nazwa = "Spółka ZOO sp. z o.o.", Adres = "ul. Grunwaldzka 8", KodPocztowy = "15-512", Miasto = "Białystok", NIP = "522-113-57-28", Telefon = "555 221 128", Email = "spolka@zoo.pl" });
                klienci.Add(new Kontrahent() { Nazwa = "Jan Niezbędny GmbH", Adres = "ul. Na Szczecin 12", KodPocztowy = "15-128", Miasto = "Białystok", NIP = "552-11-32-58", Telefon = "(85) 755 21 22" });
                klienci.Add(new Kontrahent() { Nazwa = "Stark Industries", Adres = "ul. Malmeda 15", KodPocztowy = "15-555", Miasto = "Białystok", NIP = "966-124-55-12", Email = "tony@stark.com" });

                Faktura f1 = new Faktura();
                Faktura f2 = new Faktura();
                Faktura f3 = new Faktura();

                faktury.Add(f1);
                faktury.Add(f2);
                faktury.Add(f3);

                f1.Klient = klienci[2];
                f2.Klient = klienci[0];
                f3.Klient = klienci[2];

                f1.Add(new TowarDokument(t1) { Ilosc = 2 });
                f1.Add(new TowarDokument(t2) { Ilosc = 3 });

                f2.Add(new TowarDokument(t1) { Ilosc = 3 });
                f2.Add(new TowarDokument(t3) { Ilosc = 1, CenaBrutto = 1899 });

                f3.Add(new TowarDokument(t2) { Ilosc = 3 });
                f3.Add(new TowarDokument(t1) { Ilosc = 1 });

                f1.Wprowadz();
                f2.Wprowadz();
                f3.Wprowadz();
            }
        }

        private void ApplicationExit(object sender, ExitEventArgs e)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("Sprzedawca.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, App.Current.Properties["Sprzedawca"] as Kontrahent);
            stream.Close();
            stream = new FileStream("Magazyn.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            PrawdziwyMagazyn.Instance.Serializuj(stream, formatter);
            stream.Close();
            stream = new FileStream("Klienci.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            ListaKlientow.Instance.Serializuj(stream, formatter);
            stream.Close();
            stream = new FileStream("Faktury.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            ListaFaktur.Instance.Serializuj(stream, formatter);
            stream.Close();
            stream = new FileStream("Zamowienia.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            ListaZamowien.Instance.Serializuj(stream, formatter);
            stream.Close();
        }

    }
}
