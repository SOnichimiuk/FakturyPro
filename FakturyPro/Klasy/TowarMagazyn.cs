using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FakturyPro.Klasy
{
    public class TowarMagazyn : Towar
    {
        private long id;
        private static long lastId = 0;

        private String nazwa;
        private Towar.JednostkaIlosci jednostka = Towar.JednostkaIlosci.szt;

        public TowarMagazyn()
        {
            id = ++lastId;
        }

        public TowarMagazyn(TowarMagazyn t) : this()
        {
            if (t == null)
            {
                return;
            }

            Nazwa = t.Nazwa;
            Ilosc = t.Ilosc;
            Jednostka = t.Jednostka;
            CenaNetto = t.CenaNetto;
            Vat = t.Vat;
        }

        public override String Nazwa
        {
            get { return nazwa; }
            set
            {
                nazwa = value;
                onPropertyChanged(this, "Nazwa");
            }
        }

        public override Towar.JednostkaIlosci Jednostka
        {
            get { return jednostka; }
            set
            {
                jednostka = value;
                onPropertyChanged(this, "Jednostka");
            }
        }

        public bool Equals(TowarMagazyn tm)
        {
            return tm == null ? false : id == tm.id;
        }

        [OnDeserialized()]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            if (lastId < id)
            {
                lastId = id;
            }
        }
    }
}
