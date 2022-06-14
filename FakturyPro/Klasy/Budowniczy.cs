using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FakturyPro.Klasy
{
    public interface Budowniczy
    {
        void BudujNaglowek(String naglowek);
        void BudujAdresy(Kontrahent klient, Kontrahent sprzedawca);
        void BudujTowar(int lp, TowarDokument t);
        void ZakonczTowary();
        void BudujPodpisy();
        void BudujPodliczenie(double razemBrutto);
        void DajWynik();
    }
}
