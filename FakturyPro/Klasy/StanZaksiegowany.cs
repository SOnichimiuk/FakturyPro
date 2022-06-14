using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FakturyPro.Klasy
{
    [Serializable]
    class StanZaksiegowany : StanDokumentu
    {
        public StanZaksiegowany(Dokument doc) : base(doc) { }


        public override StanDokumentu Zaksieguj(Faktura faktura)
        {
            throw new InvalidOperationException("Faktura już jest zaksięgowana");
        }

        public override StanDokumentu Wprowadz(Faktura faktura)
        {
            throw new InvalidOperationException("Faktura już jest zaksięgowana");
        }


    }
}
