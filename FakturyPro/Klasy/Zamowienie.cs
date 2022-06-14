using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FakturyPro.Klasy
{
    [Serializable]
    public class Zamowienie : Dokument
    {
        private static int ostatniNumer = 0;

        public Zamowienie()
        {
            sprzedawca = (Kontrahent)App.Current.Properties["Sprzedawca"];
            ostatniNumer++;
            NrDokumentu = "ZM/" + ostatniNumer.ToString("D4") + "/" + DataWystawienia.Year.ToString("D2");
        }

        public Faktura GenerujFakture()
        {
            Faktura fv = new Faktura() { Klient = this.Klient };
            
            foreach (TowarDokument towar in this)
            {
                fv.Add(towar);
            }

            return fv;
        }

        protected override string NazwaDokumentu
        {
            get { return "Zamówienie"; }
        }

        [OnDeserialized()]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            if (NumerDokumentu > ostatniNumer)
            {
                ostatniNumer = NumerDokumentu;
            }
            // Znajdujemy referencję na klienta
            foreach (Kontrahent k in ListaKlientow.Instance)
            {
                if (k.Equals(klient))
                {
                    klient = k;
                    return;
                }
            }
        }
    }
}
