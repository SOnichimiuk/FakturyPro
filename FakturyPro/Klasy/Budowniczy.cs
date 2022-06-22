using FakturyPro.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FakturyPro.Klasy
{
    public interface Budowniczy
    {
        void BudujNaglowek(String naglowek);
        void BudujAdresy(ClientDto klient, ClientDto sprzedawca);
        void BudujTowar(int lp, ProductDto t);
        void ZakonczTowary();
        void BudujPodpisy();
        void BudujPodliczenie(double razemBrutto);
        void DajWynik();
    }
}
