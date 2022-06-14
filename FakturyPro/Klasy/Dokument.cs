using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace FakturyPro.Klasy
{
    [Serializable]
    public abstract class Dokument : ICollection<TowarDokument>, INotifyPropertyChanged
    {
        private String nrDokumentu;
        private int numerDokumentu;
        protected Kontrahent klient;
        protected Kontrahent sprzedawca;
        private DateTime dataWystawienia;
        private List<TowarDokument> listaTowarow;

        public Dokument()
        {
            listaTowarow = new List<TowarDokument>();
            this.sprzedawca = (Kontrahent)App.Current.Properties["Sprzedawca"];
            dataWystawienia = DateTime.Now;
        }

        public virtual Kontrahent Klient
        {
            get { return klient; }
            set
            {
                klient = value;
                onPropertyChanged(this, "Klient");
            }
        }

        public virtual Kontrahent Sprzedawca
        {
            get
            {
                return sprzedawca;
            }
            protected set { sprzedawca = value; }
        }     
                        
        public DateTime DataWystawienia
        {
            get { return dataWystawienia; }
            protected set
            {
                dataWystawienia = value;
                onPropertyChanged(this, "DataWystawienia");
            }
        }
        public String NrDokumentu
        {
            get { return nrDokumentu; }
            protected set
            {
                nrDokumentu = value;
                onPropertyChanged(this, "NrDokumentu");
            }
        }


        protected int NumerDokumentu
        {
            get { return numerDokumentu; }
            set
            {
                numerDokumentu = value;
                //onPropertyChanged(this, "NumerDokumentu");
            }
        }

        public double WartoscNetto
        {
            get
            {
                double suma = 0;
                foreach (TowarDokument towar in this)
                {
                    suma += towar.WartoscNetto;
                }
                return suma;
            }
        }

        public double WartoscBrutto
        {
            get
            {
                double suma = 0;
                foreach (TowarDokument towar in this)
                {
                    suma += towar.WartoscBrutto;
                }
                return suma;
            } 
        }


        protected abstract string NazwaDokumentu
        {
            get;
        }

        public void Buduj(Budowniczy b)
        {
            b.BudujNaglowek(NazwaDokumentu + " " + NrDokumentu);
            b.BudujAdresy(klient, sprzedawca);
            int lp = 1;
            foreach (TowarDokument t in this)
            {
                b.BudujTowar(lp, t);
                lp++;
            }
            b.ZakonczTowary();
            b.BudujPodliczenie(WartoscBrutto);
            b.BudujPodpisy();
            b.DajWynik();
        }

        public void Add(TowarDokument item)
        {
            listaTowarow.Add(item);
        }

        public void Clear()
        {
            listaTowarow.Clear();
        }

        public bool Contains(TowarDokument item)
        {
            return listaTowarow.Contains(item);
        }

        public void CopyTo(TowarDokument[] array, int arrayIndex)
        {
            listaTowarow.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return listaTowarow.Count; }
        }

        public virtual bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(TowarDokument item)
        {
            return listaTowarow.Remove(item);
        }

        public IEnumerator<TowarDokument> GetEnumerator()
        {
            return listaTowarow.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return listaTowarow.GetEnumerator();
        }

        [field:NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        // OnPropertyChanged will raise the PropertyChanged event passing the
        // source property that is being updated.
        protected void onPropertyChanged(object sender, string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(sender, new PropertyChangedEventArgs(propertyName));
            }
        }

        public bool Equals(Dokument d)
        {
            return d == null ? false : NrDokumentu == d.NrDokumentu;
        }

    }
}
