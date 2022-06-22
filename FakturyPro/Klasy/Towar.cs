using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FakturyPro.Klasy
{
    public abstract class Towar : IDataErrorInfo, INotifyPropertyChanged
    {
        protected double ilosc;
        private Towar.StawkaVat vat = Towar.StawkaVat.Vat23;
        private double cenaNetto;

        public abstract String Nazwa
        {
            get;
            set;
        }

        public virtual double Ilosc
        {
            get { return ilosc; }
            set
            {
                ilosc = value;
                onPropertyChanged(this, "Ilosc");
            }
        }

        public abstract JednostkaIlosci Jednostka
        {
            get;
            set;
        }

        public StawkaVat Vat
        {
            get { return vat; }
            set
            {
                vat = value;
                onPropertyChanged(this, "Vat");
                onPropertyChanged(this, "CenaBrutto");
            }
        }

        public double CenaNetto
        {
            get { return cenaNetto; }
            set
            {
                cenaNetto = value;
                onPropertyChanged(this, "CenaNetto");
                onPropertyChanged(this, "CenaBrutto");
            }
        }

        public double CenaBrutto
        {
            get { return cenaNetto * (100 + (int)Vat) / (double)100; }
            set
            {
                CenaNetto = value / ((100 + (int)Vat) / (double)100);
            }
        }

        public double WartoscNetto
        {
            get { return Ilosc * CenaNetto; }
        }

        public double WartoscBrutto
        {
            get { return Ilosc * CenaBrutto; }
        }

        public enum JednostkaIlosci
        {
            szt,
            op,
            kg
        };

        public enum StawkaVat
        {
            Vat5 = 5,
            Vat8 = 8,
            Vat23 = 23
        };

        public virtual string this[string columnName]
        {
            get
            {
                if (columnName == "CenaNetto")
                {
                    if (cenaNetto <= 0)
                        return "Cena musi być większa od 0";
                }
                if (columnName == "CenaBrutto")
                {
                    if (cenaNetto <= 0)
                        return "Cena musi być większa od 0";
                }
                if (columnName == "Nazwa")
                {
                    if (String.IsNullOrEmpty(Nazwa) || String.IsNullOrWhiteSpace(Nazwa))
                        return "Nazwa nie może być pusta";
                }
                return null;
            }
        }

        public virtual string Error { get { return null; } }

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
    }
}
