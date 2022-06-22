using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace FakturyPro.Klasy
{
    public class Kontrahent : IDataErrorInfo, INotifyPropertyChanged
    {
        private long id;
        private static long lastId = 0;

        private String nazwa = string.Empty;
        private String adres = string.Empty;
        private String miasto = string.Empty;
        private String kodPocztowy = string.Empty;
        private String nip = string.Empty;
        private String email = string.Empty;
        private String telefon = string.Empty;

        public Kontrahent()
        {
            id = ++lastId;
        }

        public Kontrahent(Kontrahent k)
            : this()
        {
            if (k == null)
            {
                throw new ArgumentNullException();
            }

            id = k.id;
            nazwa = k.nazwa;
            adres = k.adres;
            miasto = k.miasto;
            kodPocztowy = k.kodPocztowy;
            nip = k.nip;
            email = k.email;
            telefon = k.telefon;
        }

        public String Nazwa
        {
            get { return nazwa; }
            set
            {
                nazwa = value;
                onPropertyChanged(this, "Nazwa");
            }
        }

        public String Adres
        {
            get { return adres; }
            set
            {
                adres = value;
                onPropertyChanged(this, "Adres");
            }
        }

        public String Miasto
        {
            get { return miasto; }
            set
            {
                miasto = value;
                onPropertyChanged(this, "Miasto");
            }
        }

        public String KodPocztowy
        {
            get { return kodPocztowy; }
            set
            {
                kodPocztowy = value;
                onPropertyChanged(this, "KodPocztowy");
            }
        }

        public String NIP
        {
            get { return nip; }
            set { nip = value; onPropertyChanged(this, "NIP"); }
        }

        public String Email
        {
            get { return email; }
            set { email = value; onPropertyChanged(this, "Email"); }
        }

        public String Telefon
        {
            get { return telefon; }
            set { telefon = value; onPropertyChanged(this, "Telefon"); }
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "KodPocztowy")
                {
                    String sPattern = "^\\d{2}-\\d{3}$";
                    if (!System.Text.RegularExpressions.Regex.IsMatch(kodPocztowy, sPattern))
                        return "Nieprawidłowy kod pocztowy";
                }
                if(columnName == "NIP")
                {
                    String sPattern = "^\\d{3}-\\d{3}-\\d{2}-\\d{2}$";
                    if (!System.Text.RegularExpressions.Regex.IsMatch(nip, sPattern))
                        return "Nieprawidłowy NIP";
                }
                if (columnName == "Nazwa")
                {
                    if (String.IsNullOrEmpty(nazwa) || String.IsNullOrWhiteSpace(nazwa))
                        return "Nazwa nie może być pusta";
                }
                if (columnName == "Adres")
                {
                    if (String.IsNullOrEmpty(adres) || String.IsNullOrWhiteSpace(adres))
                        return "Adres nie może być pusty";
                }
                if (columnName == "Miasto")
                {
                    if (String.IsNullOrEmpty(miasto) || String.IsNullOrWhiteSpace(miasto))
                        return "Miasto nie może być puste";
                }
                return null;
            }
        }
        public string Error { get { return null; } }

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

        public bool Equals(Kontrahent k)
        {
            return k == null ? false : id == k.id;
        }

        [OnDeserialized()]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            if (id > lastId)
            {
                lastId = id;
            }
        }
    }
}
