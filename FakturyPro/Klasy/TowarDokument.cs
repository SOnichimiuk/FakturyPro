using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;
using FakturyPro.Data.Models;

namespace FakturyPro.Klasy
{
    public class TowarDokument : Towar, IEquatable<TowarDokument>, IEquatable<TowarMagazyn>, IDataErrorInfo
    {
        private TowarMagazyn towar;

        public TowarDokument(TowarMagazyn towar)
        {
            if (towar == null)
            {
                throw new ArgumentNullException();
            }
            this.towar = towar;
            CenaNetto = towar.CenaNetto;
        }

        public override string Nazwa
        {
            get { return towar.Nazwa; }
            set { throw new NotSupportedException("Nie możesz zmienić nazwy towaru na fakturze"); }
        }

        public override double Ilosc
        {
            get { return ilosc; }
            set
            {
                /*if (value < 1)
                {
                    throw new ArgumentOutOfRangeException();
                }*/
                // Ponieważ TowarDokument jest używany w Zamówieniu oraz Fakturze
                // to wychodzi na to, że jest automatyczne rezerwacja
                // Nie jest to złe, bo pozwalamy na ujemne stany magazynowe :)
                //towar.Ilosc += ilosc - value; // Pomniejsz stan towaru w magazynie
                ilosc = value;
                onPropertyChanged(this, "Ilosc");
            }
        }

        public void ZdejmijZeStanu()
        {
            towar.Ilosc -= Ilosc;
        }

        public void ZwrocNaStan()
        {
            towar.Ilosc += Ilosc;
        }

        public void WprowadzNaStan()
        {
            towar.Ilosc += Ilosc;
        }

        public override Towar.JednostkaIlosci Jednostka
        {
            get { return towar.Jednostka; }
            set { throw new NotSupportedException("Nie możesz zmienić jednostki towaru na fakturze"); }
        }

        public bool Equals(TowarMagazyn t)
        {
            return towar.Equals(t);
        }

        public bool Equals(TowarDokument t)
        {
            return t == null ? false : towar == t.towar;
        }

        public override string this[string columnName]
        {
            get
            {
                String super = base[columnName];
                if (super != null)
                {
                    return super;
                }

                if (columnName == "Ilosc")
                {
                    if (ilosc < 1)
                        return "Ilość musi być większa od 0";
                }
                return null;
            }
        }
        public override string Error { get { return null; } }

        [OnDeserialized()]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            // Znajdź towar na magazynie, żeby przechowywać aktualną referencję
            foreach (TowarMagazyn tm in PrawdziwyMagazyn.Instance)
            {
                if (tm.Equals(towar))
                {
                    towar = tm;
                    return;
                }
            }
        }

        
    }
}
