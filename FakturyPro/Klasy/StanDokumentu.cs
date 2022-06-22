using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FakturyPro.Klasy
{
    public abstract class StanDokumentu 
    {
        protected Dokument dokument;

        public StanDokumentu(Dokument doc)
        {
            if (doc == null)
            {
                throw new ArgumentNullException();
            }
            dokument = doc;
        }

        public virtual bool IsReadOnly()
        {
            return true;
        }

        public virtual Kontrahent SetKlient(Kontrahent value)
        {
            throw new InvalidOperationException();
        }

        public virtual Kontrahent SetSprzedawca(Kontrahent value)
        {
            throw new InvalidOperationException();
        }

        public virtual StanDokumentu Wprowadz(Faktura faktura)
        {
            throw new NotImplementedException();
        }

        public virtual StanDokumentu Usun(Faktura faktura)
        {
            throw new NotImplementedException();
        }

        public virtual StanDokumentu Zaksieguj(Faktura faktura)
        {
            throw new NotImplementedException();
        }

        public virtual bool CanEdit
        {
            get { return false; }
        }
    }
}
