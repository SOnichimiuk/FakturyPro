using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FakturyPro.Klasy
{
    [Serializable]
    public class Faktura : Dokument
    {
        // Stan faktury - zaksiegowana lub nie
        private StanDokumentu stan;
        private DateTime dataSprzedazy;

        private static int ostatniNumer = 0;

        public Faktura()
        {
            stan = new StanUtworzony(this);
            sprzedawca = (Kontrahent)App.Current.Properties["Sprzedawca"];
            ostatniNumer++;
            NrDokumentu = "FV/" + ostatniNumer.ToString("D4") + "/" + DataWystawienia.Year.ToString("D2");
            NumerDokumentu = ostatniNumer;
        }

        public DateTime DataSprzedazy
        {
            get { return dataSprzedazy; }
            set
            {
                onPropertyChanged(this, "DataSprzedazy");
                dataSprzedazy = value;
            }
        }

        public StanDokumentu Stan
        {
            get { return stan; }
        }

        public override bool IsReadOnly
        {
            get { return stan.IsReadOnly(); }
        }

        public override Kontrahent Klient
        {
            get { return klient; }
            set
            {
                // Kopiuj kontrahenta, żeby zmiana danych w systemie
                // nie powodowała zmiany danych na fakturze
                klient = new Kontrahent(stan.SetKlient(value));
                onPropertyChanged(this, "Klient");
            }
        }

        public override Kontrahent Sprzedawca
        {
            get
            {
                return base.Sprzedawca;
            }
            protected set
            {
                sprzedawca = new Kontrahent(stan.SetSprzedawca(value));
                onPropertyChanged(this, "Sprzedawca");
            }
        }  

        public void Zaksieguj()
        {
            stan = stan.Zaksieguj(this);
        }

        public void Usun()
        {
            stan = stan.Usun(this);
        }

        public void Wprowadz()
        {
            stan = stan.Wprowadz(this);
        }

        public bool CanEdit
        {
            get { return stan.CanEdit; }
        }

        protected override string NazwaDokumentu
        {
            get { return "Faktura"; }
        }

        [OnDeserialized()]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            if (NumerDokumentu > ostatniNumer)
            {
                ostatniNumer = NumerDokumentu;
            }
            // Nie potrzebujemy szukać referencji na klienta, ponieważ zmiana klienta w bazie
            // nie powinna powodować zmiany danych na fakturze
        }
    }
}
