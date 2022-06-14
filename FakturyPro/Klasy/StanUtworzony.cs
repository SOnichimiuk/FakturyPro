using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FakturyPro.Klasy
{
    class StanUtworzony : StanDokumentu
    {
        public StanUtworzony(Dokument doc) : base(doc) { }

        public override bool IsReadOnly()
        {
            return true;
        }

        public override Kontrahent SetKlient(Kontrahent value)
        {
            return value;
        }

        public override Kontrahent SetSprzedawca(Kontrahent value)
        {
            return value;
        }

        private void ZdejmijZeStanu()
        {
            foreach (TowarDokument td in dokument)
            {
                td.ZdejmijZeStanu();
            }
        }
        

        public override StanDokumentu Wprowadz(Faktura faktura)
        {
            ZdejmijZeStanu();
            return new StanWprowadzony(faktura);
        }
    }
}
