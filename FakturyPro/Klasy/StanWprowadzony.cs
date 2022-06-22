using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FakturyPro.Klasy
{
    class StanWprowadzony : StanDokumentu
    {
        public StanWprowadzony(Dokument doc) : base(doc) { }

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

        public override StanDokumentu Usun(Faktura faktura)
        {
            ZwrocNaStan();
            return new StanUtworzony(faktura);
        }

        private void ZwrocNaStan()
        {
            foreach (TowarDokument td in dokument)
            {
                td.ZwrocNaStan();
            }
        }

        public override StanDokumentu Zaksieguj(Faktura faktura)
        {
            return new StanZaksiegowany(faktura);
        }

    }
}
